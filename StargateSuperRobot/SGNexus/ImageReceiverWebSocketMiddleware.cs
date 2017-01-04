using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SGNexus
{
    public class ImageReceiverWebSocketMiddleware
    {
        public static List<SocketConnections> Connections { get; set; }

        readonly RequestDelegate mNext;

        static ImageReceiverWebSocketMiddleware()
        {
            Connections = new List<SocketConnections>();
        }

        public ImageReceiverWebSocketMiddleware(RequestDelegate next)
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
                    var existigsocketconnection = ImageReceiverWebSocketMiddleware.Connections.Where(x => x.DeviceId.Equals(deviceid)).FirstOrDefault();
                    if (existigsocketconnection != null)
                    {
                        ImageReceiverWebSocketMiddleware.Connections.Remove(existigsocketconnection);
                    }
                    Connections.Add(new SocketConnections { DeviceId = deviceid, SocketConnection = webSocket });
                    while (webSocket.State == WebSocketState.Open)
                    {
                        var buffer = new ArraySegment<Byte>(new Byte[4096]);
                        var received = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

                        switch (received.MessageType)
                        {
                            case WebSocketMessageType.Close:
                                var socket = Connections.Where(x => x.SocketConnection == webSocket).First();
                                Connections.Remove(socket);
                                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed in server by the client", CancellationToken.None);
                                continue;
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
