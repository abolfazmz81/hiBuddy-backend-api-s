namespace IAM.Infrastructure.hasher;

public interface IHasher
{
    String Hash(String code);
}