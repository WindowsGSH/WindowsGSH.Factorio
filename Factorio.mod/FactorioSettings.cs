using System.Text.Json;
using System.Text.Json.Nodes;
using WindowsGSH.Core.Modules;
using WindowsGSH.Core.Servers;

namespace WindowsGSH.Modules.Factorio;

internal static class FactorioSettings
{
    private static string PathFor(string installPath) => Path.Combine(installPath, "data", "server-settings.json");

    public static async Task<IReadOnlyDictionary<string, object?>> ReadAsync(string installPath, CancellationToken cancellationToken)
    {
        var path = PathFor(installPath);
        if (!File.Exists(path)) return new Dictionary<string, object?>();
        await using var stream = File.OpenRead(path);
        var root = await JsonNode.ParseAsync(stream, cancellationToken: cancellationToken) as JsonObject;
        if (root is null) return new Dictionary<string, object?>();

        var values = new Dictionary<string, object?>();
        CopyString(root, "name", values, "server.name");
        CopyString(root, "description", values, "server.description");
        CopyString(root, "game_password", values, "server.password");
        CopyInt(root, "max_players", values, "server.maxPlayers");
        CopyBool(root, "auto_pause", values, "server.autoPause");
        CopyInt(root, "autosave_interval", values, "server.autosaveInterval");
        if (root["visibility"] is JsonObject visibility)
        {
            CopyBool(visibility, "public", values, "server.visibilityPublic");
            CopyBool(visibility, "lan", values, "server.visibilityLan");
        }
        return values;
    }

    public static async Task WriteAsync(ServerInstance instance, CancellationToken cancellationToken)
    {
        var path = PathFor(instance.InstallPath);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var root = await LoadAsync(path, cancellationToken);
        root["name"] = Get(instance, "server.name", "WindowsGSH Factorio Server");
        root["description"] = Get(instance, "server.description", "");
        root["game_password"] = Get(instance, "server.password", "");
        root["max_players"] = GetInt(instance, "server.maxPlayers", 0, 0, 65535);
        root["auto_pause"] = GetBool(instance, "server.autoPause", true);
        root["autosave_interval"] = GetInt(instance, "server.autosaveInterval", 10, 1, 1440);
        var visibility = root["visibility"] as JsonObject ?? new JsonObject();
        visibility["public"] = GetBool(instance, "server.visibilityPublic", false);
        visibility["lan"] = GetBool(instance, "server.visibilityLan", true);
        root["visibility"] = visibility;
        await File.WriteAllTextAsync(path, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }) + Environment.NewLine, cancellationToken);
    }

    private static async Task<JsonObject> LoadAsync(string path, CancellationToken cancellationToken)
    {
        if (!File.Exists(path)) return new JsonObject();
        await using var stream = File.OpenRead(path);
        return await JsonNode.ParseAsync(stream, cancellationToken: cancellationToken) as JsonObject ?? new JsonObject();
    }

    private static void CopyString(JsonObject source, string sourceKey, IDictionary<string, object?> target, string targetKey)
    { if (source[sourceKey] is JsonValue value && value.TryGetValue<string>(out var parsed)) target[targetKey] = parsed; }
    private static void CopyInt(JsonObject source, string sourceKey, IDictionary<string, object?> target, string targetKey)
    { if (source[sourceKey] is JsonValue value && value.TryGetValue<int>(out var parsed)) target[targetKey] = parsed; }
    private static void CopyBool(JsonObject source, string sourceKey, IDictionary<string, object?> target, string targetKey)
    { if (source[sourceKey] is JsonValue value && value.TryGetValue<bool>(out var parsed)) target[targetKey] = parsed; }
    private static string Get(ServerInstance instance, string key, string fallback) => instance.Settings.TryGetValue(key, out var value) && value is not null ? value.ToString()!.Trim() : fallback;
    private static int GetInt(ServerInstance instance, string key, int fallback, int minimum, int maximum) => int.TryParse(Get(instance, key, fallback.ToString()), out var value) && value >= minimum && value <= maximum ? value : fallback;
    private static bool GetBool(ServerInstance instance, string key, bool fallback) => bool.TryParse(Get(instance, key, fallback.ToString()), out var value) ? value : fallback;
}
