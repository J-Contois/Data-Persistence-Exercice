public class Player
{
    public string PlayerName { get; set; }
    public int Level { get; set; }
    public int Score { get; set; }

    public Player() { }

    public Player(string name)
    {
        PlayerName = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();
        Level = 1;
        Score = 0;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
