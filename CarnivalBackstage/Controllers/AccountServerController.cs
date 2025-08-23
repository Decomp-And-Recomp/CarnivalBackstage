using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Nodes;

namespace CarnivalBackstage.IDServer;

[Route("/account")]
[ApiController]
public class AccountServerController : Controller
{
    [Route("")]
    public async Task<IActionResult> Main([FromQuery] string action, [FromQuery] int random)
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        string body = await reader.ReadToEndAsync();

        //JsonNode node = JsonNode.Parse(body)!;

        if (random == 0) return BadRequest();

        if (action == "comavataraccount/GetAccount")
        {
            JsonObject node = new();

            node["dataServer"] = "";
            node["fileServer"] = "";
            node["isNew"] = "true";
            node["nowTime"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            return Content(node.ToJsonString());
        }
        else if (action == "comavataraccount/friend/ListFriends")
        {
            JsonObject node = new();
            node["datas"] = new JsonArray();

            return Content(node.ToJsonString());
        }

        Console.WriteLine(body);
        Console.WriteLine(action);

        return Ok();
    }
}
