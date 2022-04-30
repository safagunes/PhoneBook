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
        public ContactService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("contactservice");
        }
        public async Task<ContactDetailDto> GetContactDetailAsync(GetContact request)
        {
            try
            {
                var querystring = GetQueryString(request);  
                var httpResponseMessage = await _httpClient.GetAsync($"/contact?{querystring}");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var errorStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    var ex = await JsonSerializer.DeserializeAsync<ResponseOfException>(errorStream);
                    throw new ServiceException(ex.Code, ex.Message);
                }

                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var contentResponse = await JsonSerializer.DeserializeAsync<Response<ContactDetailDto>>(contentStream);
                return contentResponse.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("GetContactDetailAsync Error.", ex);
            }
        }

        public async Task<IEnumerable<ContactDto>> GetContactsAsync(GetContacts request)
        {
            try
            {
                var querystring = GetQueryString(request);
                var httpResponseMessage = await _httpClient.GetAsync($"/contact?{querystring}");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var errorStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    var ex = await JsonSerializer.DeserializeAsync<ResponseOfException>(errorStream);
                    throw new ServiceException(ex.Code, ex.Message);
                }

                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var contentResponse = await JsonSerializer.DeserializeAsync<Response<PagedData<ContactDto>>>(contentStream);
                return contentResponse.Data.Items;
            }
            catch (Exception ex)
            {
                throw new Exception("GetContactsAsync Error.", ex);
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
