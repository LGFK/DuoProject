namespace ClientCore.Helpes;
public static class ClientCache
{
    private static Dictionary<string, object> _cache = new Dictionary<string, object>();

    public static bool ContainsKey(string key)=>
        _cache.ContainsKey(key);

    public static T? Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var value) && value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    public static void Add<T>(string ket, T value)
    {
        if (value is not null)
        {
            _cache.Add(ket, value);
        }
    }

    public static void Remove(string ket)=>
        _cache.Remove(ket);

    public static void Clear()=>
        _cache.Clear();
}
