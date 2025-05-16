using Fleck;


var server = new WebSocketServer("ws://0.0.0.0:8181");
var wsConnections = new List<IWebSocketConnection>();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Accessing the default logger
var logger = app.Services.GetRequiredService<ILogger<Program>>();

server.Start(socket =>
{
    socket.OnOpen = () =>
    {
        logger.LogInformation("Connection Opened");
        wsConnections.Add(socket);
    };

    socket.OnClose = () => logger.LogInformation("Connection Closed");

    socket.OnMessage = message =>
    {
        logger.LogInformation($"Received: {message}");
        //socket.Send($"Echo: {message}");

        foreach (var wsConnection in wsConnections)
        {
            wsConnection.Send($"{message}");
        }
    };
});

app.Run();
