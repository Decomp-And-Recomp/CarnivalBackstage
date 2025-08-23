using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Nodes;

namespace CarnivalBackstage.IDServer;

[Route("/idServer")]
[ApiController]
public class IdServerController : Controller
{
    [Route("")]
    public async Task<IActionResult> Main()
    {
        try
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            string body = await reader.ReadToEndAsync();

            JsonNode node = JsonNode.Parse(body)!;

            node["tid"] = "0";
            node["serverId"] = "0";

            node["user"] = new JsonObject()
            {
                ["gamecenterId"] = string.Empty,
                ["facebookId"] = string.Empty
            };

            return Content(node.ToJsonString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }
}
