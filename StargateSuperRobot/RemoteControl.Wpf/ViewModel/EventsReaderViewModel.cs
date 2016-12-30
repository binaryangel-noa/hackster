using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using Microsoft.ServiceBus.Messaging;
using RemoteControl.Wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RemoteControl.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class EventsReaderViewModel : ViewModelBase
    {
        public delegate void BitmapAquiredEventHandler(object sender, BitmapSource e);
        public event BitmapAquiredEventHandler BitmapAquired;

        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;

        /// <summary>
        /// Initializes a new instance of the Administration class.
        /// </summary>
        public EventsReaderViewModel()
        {
            DeviceId = Globals.DEVICE_ID;

            Log = new ObservableCollection<DataItem>();

            Console.WriteLine("Receive messages.\n");
            eventHubClient = EventHubClient.CreateFromConnectionString(Globals.CONNECTIONSTRING_OWNER, iotHubD2cEndpoint);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            CancellationTokenSource cts = new CancellationTokenSource();

            System.Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting...");
            };

            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(receiveMessagesFromDeviceAsync(partition, cts.Token));
            }
        }

        private string mDeviceId;

        public string DeviceId
        {
            get { return mDeviceId; }
            set { Set(ref mDeviceId, value); }
        }

        private ObservableCollection<DataItem> mLog;

        public ObservableCollection<DataItem> Log
        {
            get { return mLog; }
            set { Set(ref mLog, value); }
        }

        private async Task receiveMessagesFromDeviceAsync(string partition, CancellationToken ct)
        {
            try
            {
                eventHubClient.GetDefaultConsumerGroup().Abort();
                await eventHubClient.GetDefaultConsumerGroup().CloseAsync();
                var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
                while (true)
                {
                    try
                    {
                        if (ct.IsCancellationRequested) break;
                        EventData eventData = await eventHubReceiver.ReceiveAsync();
                        if (eventData == null) continue;

                        string data = Encoding.UTF8.GetString(eventData.GetBytes());
                        if (eventData.Properties.ContainsKey("path"))
                        {
                            var path = eventData.Properties["path"];
                            if (String.Equals(path.ToString(), "imagefeed", StringComparison.InvariantCultureIgnoreCase))
                            {
                                var dataBytes = eventData.GetBytes();
                                MemoryStream ms = new MemoryStream(dataBytes, 0, dataBytes.Length);
                                var image = Image.FromStream(ms);
                                var oldBitmap = new Bitmap(image);
                                var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                          oldBitmap.GetHbitmap(System.Drawing.Color.Transparent),
                                          IntPtr.Zero,
                                          new Int32Rect(0, 0, oldBitmap.Width, oldBitmap.Height),
                                          null);

                                var del = BitmapAquired;
                                if (del != null)
                                {
                                    del(this, bitmapSource);
                                }
                                var picturespath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                                image.Save(Path.Combine(picturespath, "lastimagefromrover.jpp"));
                                addToLog(string.Format("Image message received"));
                            }
                        }
                        else
                        {
                            addToLog(string.Format("Message received. Partition: {0} Data: '{1}'", partition, data));
                        }
                    }
                    catch (Exception ex)
                    {
                        addToLog(ex.Message.ToString());
                    }
                    
                }
            }
            catch (Exception ex)
            {
                addToLog(ex.Message.ToString());
            }
        }

        private void addToLog(string message)
        {
            if (Log == null)
            {
                Log = new ObservableCollection<DataItem>();
            }
            Log.Insert(0, new DataItem(message));
            if (Log.Count > 100)
            {
                Log.RemoveAt(100);
            }
        }
    }
}