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

    public async Task<MediaFile> GetFile(string token, string fileName)
    {
        // check if token is valid
        String? user = _jwtChecker.get_Username(token);
        if (user is null)
        { 
            return null;
        }
        // get the desired document
        BsonDocument? file = await _mediaRepository.GetDoc(user, fileName);
        if (file.IsBsonNull)
        {
            return null;
        }
        // create media File
        MediaFile mediaFile =await _mediaRepository.CreateMedia(file);
        // return media file
        return mediaFile;
    }
}