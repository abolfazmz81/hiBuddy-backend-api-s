namespace Media.Domain;

public class Media
{
    public int Row_id { get; set; }
    public String User_name { get; set; }
    public String File_name { get; set; }
    public String Content_type { get; set; }
    public Stream Content { get; set; }

    public static Media Create(int id, string user, string fileName, string CType, Stream Content)
    {
        return new Media
        {
            Row_id = id,
            User_name = user,
            File_name = fileName,
            Content_type = CType,
            Content = Content
        };
    }
    
}