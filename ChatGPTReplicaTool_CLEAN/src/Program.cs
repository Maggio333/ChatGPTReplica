using Services;

var server = new WebSocketServerService("http://localhost:5069");
await server.StartAsync();
