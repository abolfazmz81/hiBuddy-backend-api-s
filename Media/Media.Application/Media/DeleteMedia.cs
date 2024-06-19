using Media.Application.Common;

namespace Media.Application.Media;

public class DeleteMedia : IDeleteMedia
{
    private readonly IMediaRepository _mediaRepository;

    public DeleteMedia(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }

    public async Task<string?> Delete(string username)
    {
        string? res = await _mediaRepository.Delete(username);
        return "ok";
    }
}