using Media.Contracts;

namespace Media.Application.Common;

public interface IMediaRepository
{
    public Task<String> Add(Domain.Media media);
    public Task<int> GetLastId();
}