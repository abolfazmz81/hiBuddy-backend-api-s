namespace IAM.Application.common;

public interface IInMemoryRepository
{
    void Add(String key);
    String? Get(String key);
}