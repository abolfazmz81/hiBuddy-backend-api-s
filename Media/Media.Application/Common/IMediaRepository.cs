using Media.Contracts;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Media.Application.Common;

public interface IMediaRepository
{
    public Task<String> Add(Domain.Media media);
    public Task<int> GetLastId();
    public Task<BsonDocument> GetDoc(String username, String fileName);
    public Task<MediaFile> CreateMedia(BsonDocument file);
}