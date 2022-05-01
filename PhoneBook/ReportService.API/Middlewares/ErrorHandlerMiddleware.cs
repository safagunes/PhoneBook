
using Newtonsoft.Json;
using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Core.ResponseBases;
using System.Net;
using System.Text.Json;

namespace ReportService.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                Response response;
                var httpResponse = context.Response;
                httpResponse.ContentType = "application/json";

                switch (error)
                {
                    case ValidationException e:
                        httpResponse.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        response = new ResponseOfValidation
                        {
                            Code = httpResponse.StatusCode,
                            ValidationErrors = e.ValidationErrors
                        };
                        break;
                    case BusinessException e:
                        httpResponse.StatusCode = e.Code;
                        response = new ResponseOfException
                        {
                            Code = e.Code,
                            Message = e.Message,
                            Description = e.InnerException?.Message,
                            Success = false
                        };
                        break;
                    case ServiceException e:
                        httpResponse.StatusCode = e.Code;
                        response = new ResponseOfException
                        {
                            Code = e.Code,
                            Message = e.Message,
                            Description = e.InnerException?.Message,
                            Success = false
                        };
                        break;
                    default:

                        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response = new ResponseOfException
                        {
                            Code = httpResponse.StatusCode,
                            Message = error.Message,
                            Description = error.InnerException?.Message ?? ""
                        };
                        break;
                }
                await httpResponse.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
