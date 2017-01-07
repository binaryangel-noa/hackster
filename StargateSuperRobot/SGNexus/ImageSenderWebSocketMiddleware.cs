using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SGNexus
{
    public class ImageSenderWebSocketMiddleware
    {
        readonly RequestDelegate mNext;

        public ImageSenderWebSocketMiddleware(RequestDelegate next)
        {
            mNext = next;
        }

        public async Task Invoke(HttpContext http)
        {
            if (http.WebSockets.IsWebSocketRequest && http.Request.Query.ContainsKey("device"))
            {
                var deviceid = http.Request.Query["device"].ToString();
                var webSocket = await http.WebSockets.AcceptWebSocketAsync();
                if (webSocket.State == WebSocketState.Open)
                {
                    while (webSocket.State == WebSocketState.Open)
                    {
                        var buffer = new ArraySegment<Byte>(new Byte[4096]);
                        var received = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

                        switch (received.MessageType)
                        {
                            case WebSocketMessageType.Close:
                                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed in server by the client", CancellationToken.None);
                                continue;
                            case WebSocketMessageType.Binary:
                                List<byte> data = new List<byte>(buffer.Take(received.Count));
                                while (received.EndOfMessage == false)
                                {
                                    received = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                                    data.AddRange(buffer.Take(received.Count));
                                }

                                var socketconnectionList = ImageReceiverWebSocketMiddleware.Connections.Where(x => x.DeviceId.Equals(deviceid, StringComparison.Ordinal)).ToArray();

                                foreach (var socketconnection in socketconnectionList)
                                {
                                    var destsocket = socketconnection.SocketConnection;
                                    if (destsocket.State == System.Net.WebSockets.WebSocketState.Open)
                                    {
                                        var type = WebSocketMessageType.Binary;

                                        try
                                        {
                                            await destsocket.SendAsync(new ArraySegment<byte>(data.ToArray()), type, true, CancellationToken.None);
                                        }
                                        catch (Exception ex)
                                        {
                                            AppInsights.Client.TrackException(ex);
                                        }
                                    }
                                    else
                                    {
                                        AppInsights.Client.TrackTrace("Removing closed connection");
                                        ImageReceiverWebSocketMiddleware.Connections.Remove(socketconnection);
                                    }
                                }

                                break;
                        }
                    }
                }
            }
            else
            {
                await mNext.Invoke(http);
            }
        }

        public class SocketConnections
        {
            public string DeviceId { get; set; }
            public WebSocket SocketConnection { get; set; }
        }
    }
}
