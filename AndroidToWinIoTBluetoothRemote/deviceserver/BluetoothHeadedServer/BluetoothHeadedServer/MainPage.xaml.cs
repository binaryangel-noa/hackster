using System;
using System.Diagnostics;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Gpio;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace BluetoothHeadedServer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StreamSocket socket;
        private DataWriter writer;
        private RfcommServiceProvider rfcommProvider;
        private StreamSocketListener socketListener;
        private readonly GpioPin lightPin;

        public MainPage()
        {
            var gpioController = GpioController.GetDefault();
            lightPin = gpioController.OpenPin(21);
            //lightPin.Write(GpioPinValue.Low);
            lightPin.SetDriveMode(GpioPinDriveMode.Output);
            
            this.InitializeComponent();
            this.InitializeRfcommServer();
        }

        /// <summary>
        /// Initializes the server using RfcommServiceProvider to advertise the Chat Service UUID and start listening
        /// for incoming connections.
        /// </summary>
        private async void InitializeRfcommServer()
        {
            try
            {
                rfcommProvider = await RfcommServiceProvider.CreateAsync(RfcommServiceId.FromUuid(Constants.RfcommDeviceServiceUuid));
            }
            // Catch exception HRESULT_FROM_WIN32(ERROR_DEVICE_NOT_AVAILABLE).
            catch (Exception ex) when ((uint)ex.HResult == 0x800710DF)
            {
                Debug.Write("Make sure your Bluetooth Radio is on: " + ex.Message);
                return;
            }

            // Create a listener for this service and start listening
            socketListener = new StreamSocketListener();
            socketListener.ConnectionReceived += OnConnectionReceived;
            var rfcomm = rfcommProvider.ServiceId.AsString();

            await socketListener.BindServiceNameAsync(rfcommProvider.ServiceId.AsString(),
                SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);

            // Set the SDP attributes and start Bluetooth advertising
            InitializeServiceSdpAttributes(rfcommProvider);

            try
            {
                rfcommProvider.StartAdvertising(socketListener, true);
            }
            catch (Exception e)
            {
                Debug.Write(e);
                return;
            }

            Debug.Write("Listening for incoming connections");
        }

        /// <summary>
        /// Creates the SDP record that will be revealed to the Client device when pairing occurs.
        /// </summary>
        /// <param name="rfcommProvider">The RfcommServiceProvider that is being used to initialize the server</param>
        private void InitializeServiceSdpAttributes(RfcommServiceProvider rfcommProvider)
        {
            var sdpWriter = new DataWriter();

            // Write the Service Name Attribute.
            sdpWriter.WriteByte(Constants.SdpServiceNameAttributeType);

            // The length of the UTF-8 encoded Service Name SDP Attribute.
            sdpWriter.WriteByte((byte)Constants.SdpServiceName.Length);

            // The UTF-8 encoded Service Name value.
            sdpWriter.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            sdpWriter.WriteString(Constants.SdpServiceName);

            // Set the SDP Attribute on the RFCOMM Service Provider.
            rfcommProvider.SdpRawAttributes.Add(Constants.SdpServiceNameAttributeId, sdpWriter.DetachBuffer());
        }

        private async void SendMessage(string message)
        {
            // Make sure that the connection is still up and there is a message to send
            if (socket != null)
            {
                writer.WriteInt32((int)message.Length);
                writer.WriteString(message);

                await writer.StoreAsync();
            }
            else
            {
                Debug.Write("No clients connected, please wait for a client to connect before attempting to send a message");
            }
        }

        private async void Disconnect()
        {
            if (writer != null)
            {
                writer.DetachStream();
                writer = null;
            }

            if (socket != null)
            {
                socket.Dispose();
                socket = null;
            }
            Debug.Write("Disconected");
        }

        /// <summary>
        /// Invoked when the socket listener accepts an incoming Bluetooth connection.
        /// </summary>
        /// <param name="sender">The socket listener that accepted the connection.</param>
        /// <param name="args">The connection accept parameters, which contain the connected socket.</param>
        private async void OnConnectionReceived(
            StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            Debug.WriteLine("Connection received");
            try
            {
                socket = args.Socket;
            }
            catch (Exception e)
            {
                Debug.Write(e);
                Disconnect();
                return;
            }

            // Note - this is the supported way to get a Bluetooth device from a given socket
            var remoteDevice = await BluetoothDevice.FromHostNameAsync(socket.Information.RemoteHostName);

            writer = new DataWriter(socket.OutputStream);
            var reader = new DataReader(socket.InputStream);
            bool remoteDisconnection = false;

            Debug.Write("Connected to Client: " + remoteDevice.Name);

            var value = lightPin.Read();
            if (value == GpioPinValue.High)
            {
                SendMessage($"Hi! Light is curently on!");
            }
            else
            {
                SendMessage($"Hi! Light is curently off!");
            }
            
            // Infinite read buffer loop
            while (true)
            {
                try
                {
                    // Based on the protocol we've defined, the first uint is the size of the message
                    uint readLength = await reader.LoadAsync(sizeof(uint));

                    // Check if the size of the data is expected (otherwise the remote has already terminated the connection)
                    if (readLength < sizeof(uint))
                    {
                        remoteDisconnection = true;
                        break;
                    }

                    var currentLength = reader.ReadUInt32();

                    // Load the rest of the message since you already know the length of the data expected.
                    readLength = await reader.LoadAsync(currentLength);

                    // Check if the size of the data is expected (otherwise the remote has already terminated the connection)
                    if (readLength < currentLength)
                    {
                        remoteDisconnection = true;
                        break;
                    }
                    string message = reader.ReadString(currentLength);

                    if (message.Equals("on", StringComparison.CurrentCultureIgnoreCase))
                    {
                        lightPin.Write(GpioPinValue.High);
                    }
                    else if (message.Equals("off", StringComparison.CurrentCultureIgnoreCase))
                    {
                        lightPin.Write(GpioPinValue.Low);
                    }

                    Debug.Write("Received: " + message);
                }
                // Catch exception HRESULT_FROM_WIN32(ERROR_OPERATION_ABORTED).
                catch (Exception ex) when ((uint)ex.HResult == 0x800703E3)
                {
                    Debug.Write("Client Disconnected Successfully");
                    break;
                }
            }

            reader.DetachStream();
            if (remoteDisconnection)
            {
                Disconnect();
                Debug.Write("Client disconnected");
            }
        }
    }
}