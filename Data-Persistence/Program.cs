using MongoDB.Driver;

var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("game");
await database.CreateCollectionAsync("profiles");
await database.CreateCollectionAsync("saves");

var game = new Game();
game.Run();