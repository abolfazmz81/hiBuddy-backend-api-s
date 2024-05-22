using Media.Application.Common;
using Media.Contracts;

namespace Media.Application.Media;

public class SaveMedia : ISaveMedia
{
    private readonly IJWTChecker _jwtChecker;
    private readonly IMediaRepository _mediaRepository;
    public SaveMedia(IJWTChecker jwtChecker, IMediaRepository mediaRepository)
    {
        _jwtChecker = jwtChecker;
        _mediaRepository = mediaRepository;
    }

    public async Task<string> Handle(MediaFile file,String token)
    {
        // check if token is valid
        String? user = _jwtChecker.get_Username(token);
        Console.WriteLine(user);
        if (user is null)
        {
            return "failed";
        }
        // check the files type
        if (file.ContentType.Split("/")[0] is not ("image" or "video"))
        {
            return "wrong";
        }
        // create media object to insert
        
        // save the file to the correct table(using Content_Type attribute)
        string res = await _mediaRepository.Add(file, user);
        if (res.Equals("failed"))
        {
            return "failed";
        }
        // return the address to be saved in main database
        return "ok";
    }
}