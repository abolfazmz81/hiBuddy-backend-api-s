using Media.Application.Common;
using Media.Contracts;
using MongoDB.Bson;

namespace Media.Application.Media;

public class GetMedia : IGetMedia
{
    private readonly IMediaRepository _mediaRepository;
    private readonly IJWTChecker _jwtChecker;

    public GetMedia(IMediaRepository mediaRepository, IJWTChecker jwtChecker)
    {
        _mediaRepository = mediaRepository;
        _jwtChecker = jwtChecker;
    }

    public async Task<MediaFile?> GetFile(string user, string fileName)
    {
        // get the desired document
        BsonDocument? file = await _mediaRepository.GetDoc(user, fileName);
        if (file is null)
        {
            return null;
        }
        // create media File
        MediaFile mediaFile =await _mediaRepository.CreateMedia(file);
        // return media file
        return mediaFile;
    }
}