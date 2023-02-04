using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1;

[ApiVersion("1.0")]
[Route("/api/[controller]")]
public class RossignolControllerBase : ControllerBase
{
    public const String LOG_FORMAT = "{0}() > {1}";
}