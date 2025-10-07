public class SaveGame
{
    public Player Player { get; set; } = new Player("Player");
    public DateTime LastSaveUtc { get; set; } = DateTime.UtcNow;

    public static SaveGame Default(string? name = null)
    {
        return new SaveGame
        {
            Player = new Player(name ?? "Player")
        };
    }
}
