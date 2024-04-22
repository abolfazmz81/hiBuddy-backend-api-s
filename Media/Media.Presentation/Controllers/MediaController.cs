﻿using System.Net;
using Media.Application.Media;
using Media.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Media.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[Route("Media")]
public class MediaController: ControllerBase
{
    
    private readonly ISaveMedia _saveMedia;

    public MediaController(ISaveMedia saveMedia)
    {
        _saveMedia = saveMedia;
    }
    
    [HttpPut("Save")]
    public async Task<ActionResult> Save(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }
        var mediaFile = new MediaFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            Content = file.OpenReadStream()
        };

        await _saveMedia.Handle(mediaFile);

        return Ok();
    }
}