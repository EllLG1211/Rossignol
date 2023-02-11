namespace API_WebSocket.Providers;

public interface IUserConnectionMapping
{
    public void Add(string key, Guid uid);

    public void Remove(string key, Guid uid);

    public Guid Get(string key);

    public IEnumerable<string> GetConnectionIds(Guid uid);
}