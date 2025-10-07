public class SaveGame
{
    public Player Player { get; set; }
    public DateTime LastSaveUtc { get; set; }

    public static SaveGame Default(string? name = null, string? hash = null, string? salt = null)
    {
        var player = new Player();
        if (name != null) player.Username = name;
        if (hash != null) player.PasswordHash = hash;
        if (salt != null) player.Salt = salt;

        return new SaveGame
        {
            Player = player,
            LastSaveUtc = DateTime.UtcNow
        };
    }
}
