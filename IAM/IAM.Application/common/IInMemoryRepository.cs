namespace IAM.Application.common;

public interface IInMemoryRepository
{
    void Add(String key);
    String? Check(String key,String value);
}