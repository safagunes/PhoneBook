using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Domain.Core.ResponseBases;
using ReportService.Domain.Dtos;
using ReportService.Domain.Requests;

namespace ReportService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        private readonly IMediator _mediator;
        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{ReportId}")]
        public async Task<ActionResult<Response<ReportDetailDto>>> Get([FromRoute] GetReport request)
        {
            var response = await _mediator.Send(request);
            return ApiResponse(response);
        }

        [HttpGet]
        public async Task<ActionResult<Response<PagedData<ReportDto>>>> Get([FromQuery] GetReports request)
        {
            var response = await _mediator.Send(request);
            return ApiResponse(response);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(CreateReport request)
        {
            var response = await _mediator.Send(request);
            return ApiResponse(response);
        }
    }
}
