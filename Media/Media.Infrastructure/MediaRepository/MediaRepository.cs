using Media.Application.Common;
using Media.Contracts;
using MongoDB.Driver;
using MongoDB.Bson;
namespace Media.Infrastructure.MediaRepository;

public class MediaRepository : IMediaRepository
{
    private readonly IMongoRepository _mongoRepository;

    public MediaRepository(IMongoRepository mongoRepository)
    {
        _mongoRepository = mongoRepository;
    }

    public async Task<string> Add(MediaFile file, string username)
    {
        var doc = _mongoRepository.CreateDoc(file, username).Result;
        try
        {
            await _mongoRepository.Insert(doc);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "failed";
        }
        
        Console.WriteLine(doc.ToString());
        return "ok";
    }
}