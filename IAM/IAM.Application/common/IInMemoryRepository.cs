namespace IAM.Application.common;

public interface IInMemoryRepository
{
    void Add(String key);
    Boolean Check(String key,String value);
}