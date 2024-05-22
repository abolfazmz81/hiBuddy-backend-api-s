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

    public async Task<string> Add(Domain.Media media)
    {
        var doc = await _mongoRepository.CreateDoc(media);
        try
        {
            await _mongoRepository.Insert(doc);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "failed";
        }
        return "ok";
    }

    public async Task<int> GetLastId()
    {
        var docs =await _mongoRepository.GetAllDocs();
        int max = 0;
        foreach (var VARIABLE in docs)
        {
            Console.WriteLine(VARIABLE[0]);
            if (VARIABLE[0].AsInt32 > max)
            {
                max = VARIABLE[0].AsInt32;
            }

        }

        return max + 1;
    }
}