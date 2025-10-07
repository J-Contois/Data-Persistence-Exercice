public class Player
{
    public string Username { get; set; } = "Invité";
    public string PasswordHash { get; set; } = "";
    public string Salt { get; set; } = "";
    public int Level { get; set; } = 1;
    public int Score { get; set; } = 0;

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
