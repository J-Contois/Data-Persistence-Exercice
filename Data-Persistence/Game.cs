public class Game
{
    private readonly string _saveDir = "Saves";
    private readonly string _savePath;
    private SaveGame _save;
    private readonly Menu _menu;

    public Game()
    {
        Directory.CreateDirectory(_saveDir);
        _savePath = Path.Combine(_saveDir, "save.json");

        _save = SaveService.LoadOrDefault(_savePath);
        _menu = new Menu();
    }

    public void Run()
    {
        Console.WriteLine($"Sauvegarde chargée pour: {_save.Player.Username} | Niveau {_save.Player.Level} | Score {_save.Player.Score}");

        bool running = true;
        while (running)
        {
            _menu.DisplayLogin();
            _menu.Display();

            switch (_menu.GetChoice())
            {
                case MenuOption.NewGame:
                    StartNewGame();
                    break;
                case MenuOption.Load:
                    LoadGame();
                    break;
                case MenuOption.Play:
                    PlayGame();
                    break;
                case MenuOption.Save:
                    SaveGameToFile();
                    break;
                case MenuOption.Quit:
                    running = false;
                    Console.WriteLine("Au revoir !");
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }
    }

    private void StartNewGame()
    {
        Console.Write("Nom du joueur: ");
        var name = Console.ReadLine();
        Console.WriteLine("Entrez votre mot de passe :");
        string pwd = Console.ReadLine();

        var (hash, salt) = PasswordService.HashPassword(pwd);
        Console.WriteLine($"Hash : {hash}");
        Console.WriteLine($"Salt : {salt}");

        Console.Write("\nTest de vérification - retapez le mot de passe : ");
        string input = Console.ReadLine() ?? "";

        bool ok = PasswordService.VerifyPassword(input, hash, salt);
        Console.WriteLine(ok ? "Mot de passe correct" : "Mot de passe incorrect");

        _save = SaveGame.Default(name, hash, salt);
        Console.WriteLine($"Nouvelle partie : {_save.Player.Username} (Score {_save.Player.Score})");
    }

    private void LoadGame()
    {
        _save = SaveService.LoadOrDefault(_savePath);
        Console.WriteLine($"Chargé: {_save.Player.Username} | Niveau {_save.Player.Level} | Score {_save.Player.Score}");
    }

    private void PlayGame()
    {
        _save.Player.AddScore(10);
        Console.WriteLine($"Vous jouez... Score = {_save.Player.Score}");
    }

    private void SaveGameToFile()
    {
        SaveService.Save(_savePath, _save);
        Console.WriteLine($"Sauvegardé ✓ ({_save.LastSaveUtc:yyyy-MM-dd HH:mm:ss} UTC)");
    }
}
