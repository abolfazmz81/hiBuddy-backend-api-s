using IAM.Application.common;

namespace IAM.Infrastructure.InMemoryRepository;

public class InMemoryRepository : IInMemoryRepository
{
    private readonly String _db;
    
    public void Add(string key)
    {
        throw new NotImplementedException();
    }

    public string? Check(string key,String value)
    {
        throw new NotImplementedException();
    }
}