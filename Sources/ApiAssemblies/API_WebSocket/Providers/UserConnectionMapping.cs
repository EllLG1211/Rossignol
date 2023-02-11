namespace API_WebSocket.Providers;

public class UserConnectionMapping : IUserConnectionMapping
{
    private readonly Dictionary<string, Guid> _map = new();

    public void Add(string key, Guid uid)
    {
        _map.Add(key, uid);
    }

    public void Remove(string key, Guid uid)
    {
        _map.Remove(key);
    }

    public Guid Get(string key)
    {
        return _map[key];
    }

    public IEnumerable<string> GetConnectionIds(Guid uid)
    {
        var pairList = _map.Where(pair => pair.Value.Equals(uid));
        List<string> keyList = new List<string>();
        foreach (var item in pairList)
        {
            keyList.Add(item.Key);
        }

        return keyList;
    }
}