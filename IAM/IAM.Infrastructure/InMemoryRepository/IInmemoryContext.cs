namespace IAM.Infrastructure.InMemoryRepository;

public interface IInmemoryContext
{
    void Set(String key,String value);
    String? Get(String key);
}