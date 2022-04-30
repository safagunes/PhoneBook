using Microsoft.AspNetCore.Mvc;
using ReportService.API.Helpers;
using ReportService.Domain.Core.ResponseBases;

namespace ReportService.API.Controllers
{

    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiConventionType(typeof(CustomApiConventions))]

    public class BaseController : ControllerBase
    {
        public ObjectResult ApiResponse(Response response)
        {
            var result = new ObjectResult(response);

            if (response.Code == 204 || response.Code == 201)
            {
                response.Code = 200;
                result.StatusCode = response.Code;
                return result;
            }

            result.StatusCode = response.Code;
            return result;
        }
    }
}
