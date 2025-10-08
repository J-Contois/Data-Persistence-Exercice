using Data_Persistence.Repositories;

public class Game
{
    public MongoContext Context { get; }
    private readonly Menu _menu;

    public Game()
    {
        Context = new MongoContext("mongodb://localhost:27017", "game");
        profiles = new ProfileRepository(ctx);
        saves = new SaveGameRepository(ctx);
        _menu = new Menu();
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
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
        var (name, pwd) = _menu.DisplayLogin();

        var existingSave = SaveService.LoadEncrypted(SavePath, pwd);
        if (existingSave != null && existingSave.Player.Username == name)
        {
            Console.WriteLine("Ce nom d'utilisateur existe déjà. Veuillez en choisir un autre.");
            return;
        }

        var (hash, salt) = Pbkdf2.HashPassword(pwd);
        Console.WriteLine($"Hash : {hash}");
        Console.WriteLine($"Salt : {salt}");

        Console.Write("\nTest de vérification - retapez le mot de passe : ");
        string input = Console.ReadLine() ?? "";

        bool ok = Pbk.VerifyPassword(input, hash, salt);
        Console.WriteLine(ok ? "Mot de passe correct" : "Mot de passe incorrect");

        _save = SaveGame.Default(name, hash, salt);
        Console.WriteLine($"Nouvelle partie : {_save.Player.Username} (Score {_save.Player.Score})");
    }

    private void LoadGame()
    {
        var (name, pwd) = _menu.DisplayLogin();
        var loaded = SaveService.LoadEncrypted(SavePath, pwd);
        if (loaded != null)
        {
            _save = loaded;
            Console.WriteLine($"Chargé: {_save.Player.Username} | Niveau {_save.Player.Level} | Score {_save.Player.Score}");
        }
        else
        {
            Console.WriteLine("Impossible de charger la sauvegarde.");
        }
    }

    private void PlayGame()
    {
        _save.Player.AddScore(10);
        Console.WriteLine($"Vous jouez... Score = {_save.Player.Score}");
    }

    private void SaveGameToFile()
    {
        Console.Write("Mot de passe pour chiffrer la sauvegarde : ");
        var pwd = Console.ReadLine();
        SaveService.SaveEncrypted(SavePath, _save, pwd);
        Console.WriteLine($"Sauvegardé ({_save.LastSaveUtc:yyyy-MM-dd HH:mm:ss} UTC)");
    }
}