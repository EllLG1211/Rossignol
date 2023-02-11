// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5033/api/notifications")
    .Build();

connection.Closed += async (error) =>
{
    await Task.Delay(new Random().Next(0, 5) * 1000);
    Console.WriteLine("Nouvelle tentative de connexion.");
    await connection.StartAsync();
};
connection.On("AddNotif", (DateTime heure, string message) =>
{
    Console.WriteLine($"{heure.ToString("HH:mm:ss")} > {message}");
});
await connection.StartAsync();

Console.ReadLine();