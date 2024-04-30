﻿using Media.Application.Common;
using Media.Contracts;

namespace Media.Application.Media;

public class SaveMedia : ISaveMedia
{
    private readonly IJWTChecker _jwtChecker;

    public SaveMedia(IJWTChecker jwtChecker)
    {
        _jwtChecker = jwtChecker;
    }

    public async Task<string> Handle(MediaFile file,String token)
    {
        // check if token is valid
        String? user = _jwtChecker.get_Username(token);
        user = "scot";
        if (user is null)
        {
            return "failed";
        }
        // check the files type
        if (file.ContentType.Split("/")[0] is not ("image" or "video"))
        {
            return "wrong";
        }
        // save the file to the correct table(using Content_Type attribute)
        
        // return the address to be saved in main database
        return "ok";
    }
}