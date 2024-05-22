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

    public MediaController(ISaveMedia saveMedia)
    {
        _saveMedia = saveMedia;
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
        return Ok();
    }
}