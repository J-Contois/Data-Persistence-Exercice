using System.Text.Json;

public static class SaveService
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static SaveGame LoadOrDefault(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Aucune sauvegarde trouvée. Création d'une sauvegarde par défaut.");
                var def = SaveGame.Default();
                Save(path, def);
                return def;
            }

            var json = File.ReadAllText(path);
            var loaded = JsonSerializer.Deserialize<SaveGame>(json, Options);

            if (loaded is null)
            {
                Console.WriteLine("Sauvegarde invalide. Réinitialisation.");
                var def = SaveGame.Default();
                Save(path, def);
                return def;
            }

            return loaded;
        }
        catch (JsonException)
        {
            Console.WriteLine("JSON corrompu. Réinitialisation.");
            var def = SaveGame.Default();
            Save(path, def);
            return def;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur d'accès fichier: {ex.Message}");
            var def = SaveGame.Default();
            Save(path, def);
            return def;
        }
    }

    public static void Save(string path, SaveGame save)
    {
        save.LastSaveUtc = DateTime.UtcNow;
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

        var tmp = path + ".tmp";
        var json = JsonSerializer.Serialize(save, Options);

        File.WriteAllText(tmp, json);
        File.Move(tmp, path, overwrite: true);
    }
}
