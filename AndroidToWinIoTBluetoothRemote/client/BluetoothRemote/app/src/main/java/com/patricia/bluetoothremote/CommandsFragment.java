package com.patricia.bluetoothremote;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;


public class CommandsFragment extends Fragment {
    public static CommandsFragment newInstance() {
        CommandsFragment fragment = new CommandsFragment();
        return fragment;
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_commands, container, false);

        Button buttonOn = (Button) rootView.findViewById(R.id.button_on);
        buttonOn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                sendCommand("ON");
            }
        });

        Button buttonOff = (Button) rootView.findViewById(R.id.button_off);
        buttonOff.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                sendCommand("OFF");
            }
        });

        return rootView;
    }

    private void sendCommand(String command) {
        BluetoothService bluetooth = BluetoothService.getInstance();
        boolean sent =  bluetooth.send(command);
    }
}
