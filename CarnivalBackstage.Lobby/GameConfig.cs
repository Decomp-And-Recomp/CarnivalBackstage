using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace CarnivalBackstage.Lobby;

internal static class GameConfig
{
    static string basePath = string.Empty;

    static readonly ConcurrentDictionary<string, byte[]> configs = [];
    
    public static void Init()
    {
        string? assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        if (assemblyPath == null) throw new NullReferenceException(nameof(assemblyPath));

        basePath = Path.Combine(assemblyPath, "Config");

        if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

        LoadConfigs();

        Console.WriteLine("Loading configs, path: " + basePath);
    }

    static void LoadConfigs()
    {
        configs.Clear();

        string[] files = Directory.GetFiles(basePath);

        foreach (var v in files)
        {
            string filename = Path.GetFileName(v);
            if (configs.TryAdd(filename, File.ReadAllBytes(v))) Console.WriteLine($"Loaded: '{filename}'");
            else Console.WriteLine($"Unable to load config: '{filename}'");
        }

        Console.WriteLine(configs.ContainsKey("L_1.xml"));

        Console.WriteLine($"(Re)Loaded {configs.Count} configs.");
    }

    public static bool TryGetConfig(string key, out byte[]? config)
    {
        return configs.TryGetValue(key, out config);
    }
}
