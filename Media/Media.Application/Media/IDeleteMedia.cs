namespace Media.Application.Media;

public interface IDeleteMedia
{
    public Task<String?> Delete(String username);
}