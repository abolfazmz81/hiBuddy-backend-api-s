
using Media.Contracts;

namespace Media.Application.Media;

public interface ISaveMedia
{
    Task<String> Handle(MediaFile file,String user);
}