using API_WebSocket.Clients;
using API_WebSocket.Providers;
using Microsoft.AspNetCore.SignalR;
using Model.Business.Users;

namespace API_WebSocket.Hubs;

/// <summary>
/// Faire hériter d'une interface
/// </summary>
public class NotificationHub : Hub<INotificationClient>
{
    /*private readonly IUserConnectionMapping _userConnectionMapping;

    public NotificationHub(IUserConnectionMapping userConnectionMapping)
    {
        _userConnectionMapping = userConnectionMapping;
    }*/

    public async Task SendNotif(Guid uid, DateTime heure, string message)
    {
        Console.WriteLine($"Envoi d'une notification à {Context.ConnectionId}");
        await Clients.Caller.AddNotif(heure, message);
    }

    public override Task OnConnectedAsync()
    {
        var task = base.OnConnectedAsync();
        SendNotif(new Guid(), DateTime.Now, "Jolie notification !");
        return task;
    }
}