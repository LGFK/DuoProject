using Newtonsoft.Json;

namespace Server.Helper;
public struct TimesTamp
{
    private const string? _fileName = "TmpBook.json";
    static public void SaveTimesTamp()
    {
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        using (var sw = File.CreateText(_fileName!))
        {
            js.Serialize(sw, $"{DateTime.UtcNow}");
        }
    }
    static public string GetTimesTamp()
    {
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        using (var sr =File.OpenText(_fileName!))
        {
            return (string)js.Deserialize(sr, typeof(string))!;
            //return sr.ReadToEnd();
        }
    }
}
