using InterviewTest.Abstractions;
using InterviewTest.Definitions;
using InterviewTest.Framework;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTest.Service
{
    public class DataService: IDataService
    {
        private readonly IHttpClient _httpClient;
        private readonly IConfigurationRoot _configuration;
        public DataService(IHttpClient httpClient, IConfigurationRoot configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IList<Person>> GetPersonData()
        {
            var request = new HttpRequest()
            {
                Url = _configuration["PersonUrl"]
            };

            var response = await _httpClient.Get(request);

            if (response.IsSuccess && !string.IsNullOrEmpty(response.Data))
            {
                var list = JsonConvert.DeserializeObject<IList<Person>>(response.Data);
                if (list == null)
                    return new List<Person>();

                return list;
            }
            else
                return new List<Person>();
        }

    }
}
