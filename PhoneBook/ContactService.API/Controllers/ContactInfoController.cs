using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.API.Controllers
{
    
    [Route("api/v1/[controller]")]
    public class ContactInfoController : BaseController
    {
        private readonly IMediator _mediator;
        public ContactInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(CreateContactInfo request)
        {
            var response = await _mediator.Send(request);
            return ApiResponse(response);
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> Delete(DeleteContact request)
        {
            var response = await _mediator.Send(request);
            return ApiResponse(response);
        }
    }
}
