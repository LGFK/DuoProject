using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest;
internal class ClientCache
{
    private Dictionary<string, object> _cache;
    public ClientCache() 
    {
        _cache= new Dictionary<string, object>();
    }
    public bool ContainsKey(string key)
    {   
        return _cache.ContainsKey(key);
    }
    public T? Get<T>(string key)
    {
        if(_cache.TryGetValue(key,out var value) && value is T typedValue)
        {
            return typedValue;
        }
        return default(T);
    }
    public void Add<T>(string key, T value)
    {
        if(value !=null)
            _cache[key] = value;
    }
    public void Remove(string key) 
    {
        _cache.Remove(key);
    }
    public void Clear()
    {
        _cache.Clear();
    }
}
