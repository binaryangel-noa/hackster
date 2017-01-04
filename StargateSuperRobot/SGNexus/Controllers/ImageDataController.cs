using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Threading;
using System.Text;

namespace SGNexus.Controllers
{
    [Route("api/[controller]")]
    public class ImageDataController : Controller
    {
        public class PutRequest
        {
            public string Image { get; set; }
        }

        [HttpPut("{id}")]
        public async void Put(string id, [FromBody]PutRequest imageData)
        {
var data = Convert.FromBase64String(imageData.Image);

var socketconnectionList = ImageReceiverWebSocketMiddleware.Connections.Where(x => x.DeviceId.Equals(id)).ToArray();

foreach (var socketconnection in socketconnectionList)
{
    var socket = socketconnection.SocketConnection;
    if (socket.State == System.Net.WebSockets.WebSocketState.Open)
    {
        var type = WebSocketMessageType.Text;
        var buffer = new ArraySegment<Byte>(Encoding.ASCII.GetBytes(imageData.Image));
        try
        {
            await socket.SendAsync(buffer, type, true, CancellationToken.None);
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
        }
    }
}
