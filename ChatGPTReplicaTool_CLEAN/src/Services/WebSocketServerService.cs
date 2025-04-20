
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace Services;

public class WebSocketServerService
{
    private readonly string _url;

    public WebSocketServerService(string url)
    {
        _url = url;
    }

    public async Task StartAsync()
    {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.UseWebSockets();

        app.Map("/ws", async context =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine("🔌 WebSocket connected.");
                await EchoLoop(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        });

        await app.RunAsync(_url);
    }

    private async Task EchoLoop(WebSocket socket)
    {
        var buffer = new byte[1024 * 4];
        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                return;
            }

            var request = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine("📨 Received: " + request);

            var response = HandlePayload(request);
            var responseBytes = Encoding.UTF8.GetBytes(response);
            await socket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    private string HandlePayload(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var query = root.GetProperty("query").GetString();

            if (query?.ToLower().Contains("uczelnie") == true)
            {
                return JsonSerializer.Serialize(new { result = "Znaleziono 3 uczelnie dopasowane do kryteriów." });
            }
            else if (query?.ToLower().Contains("badania") == true)
            {
                return JsonSerializer.Serialize(new { result = "Wykryto 5 projektów badawczych w obszarze AI." });
            }

            return JsonSerializer.Serialize(new { result = "Brak danych do zapytania." });
        }
        catch
        {
            return JsonSerializer.Serialize(new { error = "Nieprawidłowy JSON." });
        }
    }
}
