using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Persistence.Models;
public class Profile
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public string Username { get; set; } = default!;

    [BsonElement("password_hash")]
    public string PasswordHash { get; set; } = default!;

    [BsonElement("salt")]
    public string Salt { get; set; } = default!;

    [BsonElement("iterations")]
    public int Iterations { get; set; }

    [BsonElement("created_utc")]
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
