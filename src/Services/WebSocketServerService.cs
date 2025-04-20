using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace Services;

public class WebSocketServerService
{
    private readonly HttpListener _httpListener;

    public WebSocketServerService(string prefix = "http://localhost:5000/ws/")
    {
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add(prefix);
    }

    public async Task StartAsync()
    {
        _httpListener.Start();
        Console.WriteLine("WebSocket Server started. Waiting for connections...");

        while (true)
        {
            var context = await _httpListener.GetContextAsync();

            if (context.Request.IsWebSocketRequest)
            {
                _ = HandleClient(context);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }

    private async Task HandleClient(HttpListenerContext context)
    {
        var wsContext = await context.AcceptWebSocketAsync(null);
        var socket = wsContext.WebSocket;
        var buffer = new byte[1024];

        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye", CancellationToken.None);
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received: {message}");

                var reply = $"Echo: {message}";

                var responseBuffer = Encoding.UTF8.GetBytes(reply);
                await socket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}

