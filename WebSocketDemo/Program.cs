using Fleck;

var server = new WebSocketServer("ws://0.0.0.0:8181");

var wsConnections = new List<IWebSocketConnection>();

server.Start(socket =>
{
    socket.OnOpen = () =>
    {
        Console.WriteLine("Connection Opened");
        wsConnections.Add(socket);
    };

    socket.OnClose = () => Console.WriteLine("Connection Closed");
    socket.OnMessage = message =>
    {
        //Console.WriteLine($"Received: {message}");
        //socket.Send($"Echo: {message}");

        foreach (var wsConnection in wsConnections)
        {
            wsConnection.Send($"Echo: {message}");
        }
    };
});

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Run();