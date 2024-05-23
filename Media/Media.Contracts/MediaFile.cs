namespace Media.Contracts;

public class MediaFile
{
    public string FileName { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public Stream Content { get; set; }
}