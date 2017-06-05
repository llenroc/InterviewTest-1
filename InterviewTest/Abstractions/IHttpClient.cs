using System.Threading.Tasks;

namespace InterviewTest.Abstractions
{
    public interface IHttpClient
    {
        Task<IHttpResponse> Get(IHttpRequest request);
    }
}
