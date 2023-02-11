namespace API_WebSocket.Clients;

public interface INotificationClient
{
    Task AddNotif(DateTime time, string message);
}