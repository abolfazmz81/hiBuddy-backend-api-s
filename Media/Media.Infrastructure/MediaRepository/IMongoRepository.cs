using Media.Contracts;
using MongoDB.Bson;

namespace Media.Infrastructure.MediaRepository;

public interface IMongoRepository
{
    
    Task<BsonDocument> CreateDoc(MediaFile file, string username);
}