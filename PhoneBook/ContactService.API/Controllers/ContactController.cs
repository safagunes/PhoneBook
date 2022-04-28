using ContactService.API.Helpers;
using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.API.Controllers
{
    
    [Route("api/v1/[controller]")]
    public class ContactController : BaseController
    {
        private readonly IMediator _mediator;
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Name = "Contacts")]
        public async Task<ActionResult<Response>> Post(CreateContact createContact)
        {
            var response = await _mediator.Send(createContact);
            return ApiResponse(response);
        }
    }
}
