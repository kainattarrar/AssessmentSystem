using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentSystem.Service.API.Controllers.Base
{
    [ApiController]
    [Route("[Controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
