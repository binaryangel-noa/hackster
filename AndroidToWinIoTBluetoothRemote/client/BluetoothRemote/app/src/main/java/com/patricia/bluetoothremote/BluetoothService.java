package com.patricia.bluetoothremote;

import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.ByteBuffer;
import java.util.UUID;

public class BluetoothService {
    public static final UUID SERVICE_UUID = UUID.fromString("34B1CF4D-1069-4AD6-89B6-E161D79BE4D9");
    private static final String TAG = "BluetoothService";
    private static BluetoothService instance = new BluetoothService();
    private ConnectThread mConnectThread;
    private ConnectedThread mConnectedThread;
    private Handler mHandler; // handler that gets info from Bluetooth service

    private BluetoothService() {
        mHandler = new Handler(new Handler.Callback() {
            @Override
            public boolean handleMessage(Message msg) {
                Log.d(TAG, "got a notification in " + Thread.currentThread());
                return false;
            }
        });
    }

    public static BluetoothService getInstance() {
        return instance;
    }

    public void connectToDevice(BluetoothDevice device) {
        //close existing connections
        if (mConnectedThread != null) mConnectedThread.cancel();
        if (mConnectThread != null) mConnectThread.cancel();

        mConnectThread = new ConnectThread(device);
        mConnectThread.start();
    }

    private void manageMyConnectedSocket(BluetoothSocket mmSocket) {
        if (mmSocket.isConnected()) {
            mConnectedThread = new ConnectedThread(mmSocket);
            mConnectedThread.start();
        }
    }

    public boolean send(String command) {
        return mConnectedThread.write(command);
    }

    public void registerNewHandlerCallback(Handler.Callback callback) {
        mHandler = new Handler(callback );
    }

    // Defines several constants used when transmitting messages between the
    // service and the UI.
    public interface MessageConstants {
        public static final int MESSAGE_READ = 0;
        public static final int MESSAGE_WRITE = 1;
        public static final int MESSAGE_TOAST = 2;
    }

    private class ConnectThread extends Thread {
        private final BluetoothSocket mmSocket;
        private final BluetoothDevice mmDevice;

        public ConnectThread(BluetoothDevice device) {
            // Use a temporary object that is later assigned to mmSocket
            // because mmSocket is final.
            BluetoothSocket tmp = null;
            mmDevice = device;
            try {
                // Get a BluetoothSocket to connect with the given BluetoothDevice.
                // SERVICE_UUID is the app's UUID string, also used in the server code.
                tmp = device.createRfcommSocketToServiceRecord(SERVICE_UUID);
            } catch (IOException e) {
                Log.e(TAG, "Socket's create() method failed", e);
            }
            mmSocket = tmp;
        }

        public void run() {
            try {
                // Connect to the remote device through the socket. This call blocks
                // until it succeeds or throws an exception.
                mmSocket.connect();
            } catch (IOException connectException) {
                // Unable to connect; close the socket and return.
                try {
                    mmSocket.close();
                } catch (IOException closeException) {
                    Log.e(TAG, "Could not close the client socket", closeException);
                }
                return;
            }

            // The connection attempt succeeded. Perform work associated with
            // the connection in a separate thread.
            manageMyConnectedSocket(mmSocket);
        }

        // Closes the client socket and causes the thread to finish.
        public void cancel() {
            try {
                mmSocket.close();
            } catch (IOException e) {
                Log.e(TAG, "Could not close the client socket", e);
            }
        }
    }

    private class ConnectedThread extends Thread {
        private final BluetoothSocket mmSocket;
        private final InputStream mmInStream;
        private final OutputStream mmOutStream;

        public ConnectedThread(BluetoothSocket socket) {
            mmSocket = socket;
            InputStream tmpIn = null;
            OutputStream tmpOut = null;

            // Get the input and output streams; using temp objects because
            // member streams are final.
            try {
                tmpIn = socket.getInputStream();
            } catch (IOException e) {
                Log.e(TAG, "Error occurred when creating input stream", e);
            }
            try {
                tmpOut = socket.getOutputStream();
            } catch (IOException e) {
                Log.e(TAG, "Error occurred when creating output stream", e);
            }

            mmInStream = tmpIn;
            mmOutStream = tmpOut;
        }

        public void run() {
            // Keep listening to the InputStream until an exception occurs.
            while (true) {
                try {
                    // Read from the InputStream.
                    byte[] buffer = new byte[4];
                    int read = mmInStream.read(buffer);
                    if (read < 4) {
                        this.cancel();
                    }
                    int dataLength = ByteBuffer.wrap(buffer).getInt();

                    buffer = new byte[dataLength];
                    read = mmInStream.read(buffer);
                    if (read < dataLength) {
                        this.cancel();
                    }

                    String command = new String(buffer);

                    // Send the obtained bytes to the UI activity.
                    Message readMsg = mHandler.obtainMessage(
                            MessageConstants.MESSAGE_READ, -1, -1,
                            command);
                    readMsg.sendToTarget();
                } catch (IOException e) {
                    Log.d(TAG, "Input stream was disconnected", e);
                    break;
                }
            }
        }

        // Call this from the main activity to send data to the remote device.
        public boolean write(String command) {
            try {
                // Allocate bytes for integer indicating size and message itself
                ByteBuffer bb = ByteBuffer.allocate(4 + command.length())
                        .putInt(command.length())
                        .put(command.getBytes());

                mmOutStream.write(bb.array());
                mmOutStream.flush();

                // Share the sent message with the UI activity.
                Message writtenMsg = mHandler.obtainMessage(MessageConstants.MESSAGE_WRITE, -1, -1, command);
                writtenMsg.sendToTarget();
                return true;
            } catch (IOException e) {
                Log.e(TAG, "Error occurred when sending data", e);

                // Send a failure message back to the activity.
                Message writeErrorMsg =
                        mHandler.obtainMessage(MessageConstants.MESSAGE_TOAST);
                Bundle bundle = new Bundle();
                bundle.putString("toast",
                        "Couldn't send data to the other device");
                writeErrorMsg.setData(bundle);
                mHandler.sendMessage(writeErrorMsg);
            }
            return false;
        }

        // Call this method from the main activity to shut down the connection.
        public void cancel() {
            try {
                mmSocket.close();
            } catch (IOException e) {
                Log.e(TAG, "Could not close the connect socket", e);
            }
        }
    }
}
