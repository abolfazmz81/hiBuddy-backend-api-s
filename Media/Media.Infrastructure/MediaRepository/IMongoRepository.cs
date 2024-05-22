using Media.Contracts;
using MongoDB.Bson;

namespace Media.Infrastructure.MediaRepository;

public interface IMongoRepository
{
    Task Insert(BsonDocument doc);
    Task<BsonDocument> CreateDoc(MediaFile file, string username);
    Task<List<BsonDocument>> GetAllDocs();
}