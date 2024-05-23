using System.Net;
using Media.Application.Media;
using Media.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Media.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[Route("Media")]
public class MediaController: ControllerBase
{
    
    private readonly ISaveMedia _saveMedia;
    private readonly IGetMedia _getMedia;

    public MediaController(ISaveMedia saveMedia, IGetMedia getMedia)
    {
        _saveMedia = saveMedia;
        _getMedia = getMedia;
    }
    
    [HttpPut("Save")]
    [Authorize]
    public async Task<ActionResult> Save(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }
        var mediaFile = new MediaFile
        {
            FileName = file.FileName,
            Name = file.Name,
            ContentType = file.ContentType,
            Content = file.OpenReadStream()
        };
        string token = HttpContext.Request.Headers.Authorization;
        token = token.Split(" ")[1];
        String ou = await _saveMedia.Handle(mediaFile,token);
        if (ou.Equals("failed"))
        {
            return BadRequest("invalid token");
        }
        if (ou.Equals("wrong"))
        {
            return BadRequest("wrong type");
        }
        if (ou.Equals("bad"))
        {
            return BadRequest("an error happened, try again");
        }
        return Ok();
    }

    [HttpGet("GetMedia")]
    [Authorize]
    public async Task<ActionResult> GetMedia(String Filename)
    {
        string token = HttpContext.Request.Headers.Authorization;
        token = token.Split(" ")[1];
        var media = await _getMedia.GetFile(token, Filename);
        
        IFormFile file = new FormFile(media.Content,0,media.Content.Length,media.Name,media.FileName);
        return Ok(file);
    }
}