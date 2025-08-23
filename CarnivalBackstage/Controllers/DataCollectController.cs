using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CarnivalBackstage.DataCollectServer;

[Route("/dataCollect")]
[ApiController]
public class DataCollectController : Controller
{
    [Route("")]
    public async Task<IActionResult> Main()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        string body = await reader.ReadToEndAsync();
        Console.WriteLine(body);

        return Ok();
    }
}
