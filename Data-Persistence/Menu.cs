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
    public void DisplayLogin()
    {
        Console.WriteLine("Veuillez vous connecter à votre compte.");
        Console.WriteLine("Entrez votre nom d'utilisateur :");

        Console.WriteLine("Entrez votre mot de passe :");
        string pwd = Console.ReadLine();
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
