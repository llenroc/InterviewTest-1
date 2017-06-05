using InterviewTest.Abstractions;
using System.Threading.Tasks;

namespace InterviewTest.Framework
{
    public class HttpClient: IHttpClient
    {
        System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();

        public async Task<IHttpResponse> Get(IHttpRequest request)
        {
            var response = new HttpResponse();
                        
            var result = await _client.GetAsync(request.Url);

            response.IsSuccess = result.IsSuccessStatusCode;

            if (response.IsSuccess)
                response.Data = await result.Content.ReadAsStringAsync();

            return response;
        }
    }
}
