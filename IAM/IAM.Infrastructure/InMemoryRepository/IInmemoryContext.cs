namespace IAM.Infrastructure.InMemoryRepository;

public interface IInmemoryContext
{
    Task Set(String key,String value);
    Task<String?> Get(String key);
}