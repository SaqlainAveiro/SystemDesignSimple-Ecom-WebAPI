using Microsoft.AspNetCore.Mvc;

namespace SystemDesignSimple_Ecom_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EComController : ControllerBase
{

    private readonly ILogger<EComController> _logger;

    public EComController(ILogger<EComController> logger)
    {
        _logger = logger;
    }
}
