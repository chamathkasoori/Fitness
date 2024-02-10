using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class XeroController : CommonController<XeroController>
{
    private readonly IXeroService _xeroService;

    public XeroController(IXeroService xeroService)
    {
        _xeroService = xeroService;
    }
    
    [HttpPost("Token")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> SaveToken([FromForm] string refresh_token)
    {
        try
        {
            await _xeroService.SaveToken(new Config{XeroToken = refresh_token});
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}