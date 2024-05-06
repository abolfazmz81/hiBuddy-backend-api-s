using Media.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Media.Infrastructure.MediaRepository;

public class MongoRepository : IMongoRepository
{
    private static MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");
    private static IMongoDatabase database = client.GetDatabase("HiBuddy");
    private static IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("");

    public async Task<BsonDocument> CreateDoc(MediaFile file, string username)
    {
        BsonDocument document = new BsonDocument
        {
            {"_id",new BsonArray{"Row_id",2}},
            {"User_name",username},
            {"File_name",file.FileName},
            {"Content_Type",file.ContentType},
            {"Content",file.Content.ToString()},
        };
        return document;
    }
}