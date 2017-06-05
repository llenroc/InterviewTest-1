namespace InterviewTest.Abstractions
{
    public interface IHttpResponse
    {
        string Data { get; }
        bool IsSuccess { get; }
    }
}
