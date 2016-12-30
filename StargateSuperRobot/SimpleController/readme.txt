Start here: 
https://docs.microsoft.com/de-de/azure/iot-hub/iot-hub-csharp-csharp-getstarted

Sync time on windows iot:
Enter-PSSession -ComputerName minwinpc -Credential minwinpc\Administrator
w32tm /config /syncfromflags:manual /manualpeerlist:"0.windows.time.com 1.pool.ntp.org" 
http://stackoverflow.com/questions/30585900/how-to-set-system-time-in-windows-10-iot