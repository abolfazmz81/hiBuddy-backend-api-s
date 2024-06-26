﻿using Media.Application.Common;
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

    public async Task<BsonDocument> GetDoc(string username, string fileName)
    {
        BsonDocument doc =await _mongoRepository.GetDoc(username, fileName);
        return doc;
    }

    public async Task<MediaFile> CreateMedia(BsonDocument file)
    {
        MediaFile mediaFile = new MediaFile();
        mediaFile.Name = file[2].ToString();
        mediaFile.FileName = file[3].ToString();
        mediaFile.ContentType = file[4].ToString();
        Byte[] stream = file[5].AsByteArray;
        mediaFile.Content = new MemoryStream(stream);
        return mediaFile;
    }

    public async Task<string?> Delete(string username)
    {
        String? res = await _mongoRepository.DeleteAll(username);
        return "ok";
    }
}