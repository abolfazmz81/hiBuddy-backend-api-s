namespace IAM.Application.common;

public interface IInMemoryRepository
{
    Task Add(String key,String code);
    Task<String?> Get(String key);
}