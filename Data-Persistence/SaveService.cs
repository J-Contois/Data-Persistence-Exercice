using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public static class SaveService
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static void SaveEncrypted(Profile profile, string password)
    {
        // Sérialisation JSON
        var json = JsonSerializer.Serialize(profile, Options);
        byte[] plaintext = Encoding.UTF8.GetBytes(json);

        // Génération du sel et dérivation de la clé
        byte[] passwordHash = Convert.FromBase64String(profile.PasswordHashB64);
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        var key = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256).GetBytes(32);

        // Chiffrement AES-GCM
        byte[] nonce = RandomNumberGenerator.GetBytes(12);
        byte[] ciphertext = new byte[plaintext.Length];
        byte[] tag = new byte[16];

        using (var aes = new AesGcm(key))
        {
            aes.Encrypt(nonce, plaintext, ciphertext, tag);
        }

        var payload = new EncryptedPayload
        {
            Salt = Convert.ToBase64String(salt),
            Nonce = Convert.ToBase64String(nonce),
            Tag = Convert.ToBase64String(tag),
            Data = Convert.ToBase64String(ciphertext)
        };

        var encryptedJson = JsonSerializer.Serialize(payload, Options);

        SaveCollection.InsertOne(profile);
    }

    public static SaveGame? LoadEncrypted(Profile profile, string password)
    {
        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Aucune sauvegarde chiffrée trouvée.");
                return null;
            }

            var encryptedContent = File.ReadAllText(path);
            var payload = JsonSerializer.Deserialize<EncryptedPayload>(encryptedContent);

            if (payload == null)
                throw new Exception("Fichier de sauvegarde corrompu.");

            byte[] salt = Convert.FromBase64String(payload.Salt);
            byte[] nonce = Convert.FromBase64String(payload.Nonce);
            byte[] tag = Convert.FromBase64String(payload.Tag);
            byte[] ciphertext = Convert.FromBase64String(payload.Data);

            var key = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256).GetBytes(32);
            byte[] decrypted = new byte[ciphertext.Length];

            using (var aes = new AesGcm(key))
            {
                aes.Decrypt(nonce, ciphertext, tag, decrypted);
            }

            string json = Encoding.UTF8.GetString(decrypted);
            var save = JsonSerializer.Deserialize<SaveGame>(json, Options);
            return save;
        }
        catch (CryptographicException)
        {
            Console.WriteLine("Mot de passe incorrect ou fichier corrompu.");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du chargement : {ex.Message}");
            return null;
        }
    }
}