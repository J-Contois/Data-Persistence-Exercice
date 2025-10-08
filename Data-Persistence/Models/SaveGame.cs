using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Persistence.Models;
public class SaveGame
{
    [BsonId]
    [BsonElement("username")]
    public string Username { get; set; } = default!;

    [BsonElement("level")]
    public int Level { get; set; } = 1;

    [BsonElement("score")]
    public int Score { get; set; } = 0;

    [BsonElement("last_save_utc")]
    public DateTime LastSaveUtc { get; set; } = DateTime.UtcNow;

    public void AddScore()
    {
        Score += 10;
    }
}
