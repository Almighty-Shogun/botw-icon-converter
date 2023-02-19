using Newtonsoft.Json;

namespace BotwIconConverter.Core;

public static class Config
{
    private static readonly IDictionary<string, bool> ConfigValues = null!;

    static Config()
    {
        if(!File.Exists("config.json"))
        {
            Console.WriteLine("The (config.json) file does not exists!");
            return;
        }

        ConfigValues = JsonConvert.DeserializeObject<IDictionary<string, bool>>(File.ReadAllText("config.json"))!;
    }

    public static bool GetConfig(string key, bool fallback = false) => ConfigValues.TryGetValue(key, out var value) ? value : fallback;
}