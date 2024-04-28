namespace Media.Application.Common;

public interface IJWTChecker
{
    String? get_Username(String token);
}