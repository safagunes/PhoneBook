using Microsoft.Extensions.Configuration;
using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Core.ResponseBases;
using ReportService.Domain.Services;
using ReportService.Domain.Services.Contact.Dtos;
using ReportService.Domain.Services.Contact.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace ReportService.Infrastructure.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public ContactService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("contactservice");
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }
        public async Task<ContactDetailDto> GetContactDetailAsync(GetContact request)
        {
            try
            {
 
                var httpResponseMessage = await _httpClient.GetAsync($"/api/v1/contact/{request.ContactId}");
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    var errormessage = await httpResponseMessage.Content.ReadAsStringAsync();
                    var ex = JsonSerializer.Deserialize<ResponseOfException>(errormessage, _jsonSerializerOptions);
                    throw new ServiceException(ex.Code, ex.Message);
                }

                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var contentResponse =  JsonSerializer.Deserialize<Response<ContactDetailDto>>(content, _jsonSerializerOptions);
                return contentResponse.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("ContactDetail Get Error.", ex);
            }
        }

        public async Task<IEnumerable<ContactDto>> GetContactsAsync(GetContacts request)
        {
            try
            {
                var querystring = GetQueryString(request);
                var httpResponseMessage = await _httpClient.GetAsync($"/api/v1/contact?{querystring}");
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    var errormessage = await httpResponseMessage.Content.ReadAsStringAsync();
                    var ex = JsonSerializer.Deserialize<ResponseOfException>(errormessage, _jsonSerializerOptions);
                    throw new ServiceException(ex.Code, ex.Message);
                }

                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var contentResponse =  JsonSerializer.Deserialize<Response<PagedData<ContactDto>>>(content, _jsonSerializerOptions);
                return contentResponse.Data.Items;
            }
            catch (Exception ex)
            {
                throw new Exception("Contacts Get Error.", ex);
            }
        }
        private string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
    }
}
