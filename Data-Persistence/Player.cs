public class Player
{
    public string Username { get; set; }
    public int Level { get; set; }
    public int Score { get; set; }


    public string Salt { get; set; }

    public string PasswordHash { get; set; }

    public Player() { }

    public Player(string name)
    {
        Username = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();
        Level = 1;
        Score = 0;
        Salt = null;
        PasswordHash = null;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
