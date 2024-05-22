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

    public async Task<BsonDocument> CreateDoc(Domain.Media media)
    {
        var memory = new MemoryStream();
        await media.Content.CopyToAsync(memory);
        byte[] image = memory.ToArray();
        BsonDocument document = new BsonDocument
        {
            {"_id",media.Row_id},
            {"User_name",media.User_name},
            {"File_name",media.File_name},
            {"Content_Type",media.Content_type},
            {"Content",image},
        };
        return document;
    }

    public async Task<List<BsonDocument>> GetAllDocs()
    {
        var docs = collection.Find(new BsonDocument()).ToList();
        return docs;
    }
}