using ContactService.API.Helpers;
using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Dtos;
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
        

        [HttpGet("{ContactId}")]
        public async Task<ActionResult<Response<ContactDetailDto>>> Get(GetContact request)
        {
            var response = await _mediator.Send(request);
            return ApiResponse(response);
        }

        [HttpGet]
        public async Task<ActionResult<Response<PagedData<ContactDto>>>> Get(GetContacts request)
        {
            var response = await _mediator.Send(request);
            return ApiResponse(response);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(CreateContact request)
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
