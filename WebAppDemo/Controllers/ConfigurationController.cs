using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAppDemo.Configuration;
using WebAppDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConfigurationController : ControllerBase
{
    private readonly IMyConfigurationService _configService;

    public ConfigurationController(IMyConfigurationService configService)
    {
        _configService = configService;
    }

    // GET: api/<ConfigurationController>
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return new string[] {
            _configService.Key,
            _configService.IsFlagged.ToString(),
            _configService.Number.ToString()
        };
    }
}
