namespace IAM.Application.common;

public interface IInMemoryRepository
{
    void Add(String key,String code);
    String? Get(String key);
}