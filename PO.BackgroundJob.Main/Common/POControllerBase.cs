using Microsoft.AspNetCore.Mvc;

namespace PO.BackgroundJob.Main.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class POControllerBase: ControllerBase
    {
    }
}
