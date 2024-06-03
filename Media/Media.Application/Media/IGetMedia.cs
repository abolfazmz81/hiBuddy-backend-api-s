using Media.Contracts;

namespace Media.Application.Media;

public interface IGetMedia
{
    public Task<MediaFile?> GetFile(String user, String fileName);
}