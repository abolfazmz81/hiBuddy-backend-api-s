using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Media.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[Route("Media")]
public class MediaController: ControllerBase
{
    
    [HttpPut("Save")]
    public async Task<ActionResult> Save(IFormFile file)
    {
        //Stream stream = new FileStream(Path.Combine("../Media.Infrastructure/files",file.FileName), FileMode.Create);
        //await file.CopyToAsync(stream);
        
        Console.WriteLine(file.FileName);
        return Ok();
    }
}