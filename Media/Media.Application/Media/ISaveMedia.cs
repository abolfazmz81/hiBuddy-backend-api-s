
using Media.Contracts;

namespace Media.Application.Media;

public interface ISaveMedia
{
    Task Handle(MediaFile file);
}