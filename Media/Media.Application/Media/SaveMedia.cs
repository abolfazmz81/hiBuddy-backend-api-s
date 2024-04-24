using Media.Contracts;

namespace Media.Application.Media;

public class SaveMedia : ISaveMedia
{
    public async Task<string> Handle(MediaFile file)
    {
        // check if token is valid
        
        // check the files type
        
        // save the file to the correct table
        
        // return the address to be saved in main database
        return "ok";
    }
}