using Media.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Media.Infrastructure.MediaRepository;

public class MongoRepository : IMongoRepository
{
    private static MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");
    private static IMongoDatabase database = client.GetDatabase("HiBuddy");
    private static IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Media");

    public async Task Insert(BsonDocument doc)
    {
        await collection.InsertOneAsync(doc);
    }

    public async Task<BsonDocument> CreateDoc(MediaFile file, string username)
    {
        var memory = new MemoryStream();
        await file.Content.CopyToAsync(memory);
        byte[] image = memory.ToArray();
        BsonDocument document = new BsonDocument
        {
            {"_id",2},
            {"User_name",username},
            {"File_name",file.FileName},
            {"Content_Type",file.ContentType},
            {"Content",image},
        };
        return document;
    }
}