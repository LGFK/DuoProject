using Newtonsoft.Json;

namespace ClientCore.Helpes;
public struct TimesTamp
{
    private const string? _fileTimesTamp = "TmpTimesTamp.json";
    static public void SaveTimesTamp()
    {
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        using (var sw = File.CreateText(_fileTimesTamp!))
        {
            js.Serialize(sw, $"{DateTime.UtcNow}");
        }
    }

    static public string GetTimesTamp()
    {
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        if (!File.Exists(_fileTimesTamp))
        {
            return "Nope";

        }

        using (var sr = File.OpenText(_fileTimesTamp!))
        {
            return (string)js.Deserialize(sr, typeof(string))!;
            //return sr.ReadToEnd();
        }
    }

    internal static void SaveTimesTamp(string? timesTamp)
    {
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        using (var sw = File.CreateText(_fileTimesTamp!))
        {
            js.Serialize(sw, $"{timesTamp}.json");
        }
    }
}

