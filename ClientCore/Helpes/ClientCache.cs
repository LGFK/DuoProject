namespace ClientCore.Helpes;
internal class ClientCache
{
    private Dictionary<string, object> _cache;
    public ClientCache()
    {
        _cache = new Dictionary<string, object>();
    }

    public bool ContainsKey(string key)
    {
        return _cache.ContainsKey(key);
    }

    public T? Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var value) && value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    public void Add<T>(string ket, T value)
    {
        if (value is not null)
        {
            _cache.Add(ket, value);
        }
    }

    public void Remove(string ket)
    {
        _cache.Remove(ket);
    }

    public void Clear()
    {
        _cache.Clear();
    }
}
