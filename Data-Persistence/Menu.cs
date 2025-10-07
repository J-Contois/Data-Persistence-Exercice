public enum MenuOption
{
    Invalid,
    NewGame,
    Load,
    Play,
    Save,
    Quit
}

public class Menu
{

    public (string username, string password) DisplayLogin()
    {
        Console.WriteLine("Veuillez vous connecter à votre compte.");
        Console.Write("Entrez votre nom d'utilisateur : ");
        string username = Console.ReadLine() ?? "";

        Console.Write("Entrez votre mot de passe : ");
        string password = Console.ReadLine() ?? "";

        return (username, password);
    }

    public void Display()
    {
        Console.WriteLine("\n=== Menu ===");
        Console.WriteLine("1. Nouvelle partie");
        Console.WriteLine("2. Charger");
        Console.WriteLine("3. Jouer (+10 score)");
        Console.WriteLine("4. Sauvegarder");
        Console.WriteLine("0. Quitter");
    }

    public MenuOption GetChoice()
    {
        Console.Write("> Votre choix: ");
        var input = Console.ReadLine()?.Trim();

        return input switch
        {
            "1" => MenuOption.NewGame,
            "2" => MenuOption.Load,
            "3" => MenuOption.Play,
            "4" => MenuOption.Save,
            "0" => MenuOption.Quit,
            _ => MenuOption.Invalid
        };
    }
}
