using Media.Application.Common;
using Media.Contracts;

namespace Media.Infrastructure.MediaRepository;

public class MediaRepository : IMediaRepository
{
    public async Task<string> Add(MediaFile file, string username)
    {
        throw new NotImplementedException();
    }
}