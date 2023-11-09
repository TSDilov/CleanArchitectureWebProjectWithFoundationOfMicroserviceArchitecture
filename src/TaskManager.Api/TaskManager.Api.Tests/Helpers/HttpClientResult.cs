using System.Net;

namespace TaskManager.Api.Tests.Helpers
{
    public class HttpClientResult<TSuccess, TError>
    {
        public TSuccess Success { get; private set; }

        public TError Error { get; private set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool InSuccess()
        {
            return HttpStatusCode.OK <= StatusCode && StatusCode < HttpStatusCode.Ambiguous; 
        }

        public static HttpClientResult<TSuccess, TError> CreateSuccess(TSuccess data, HttpStatusCode statusCode)
        {
            return new HttpClientResult<TSuccess, TError>
            {
                Success = data,
                StatusCode = statusCode
            };
        }

        public static HttpClientResult<TSuccess, TError> CreateError(TError error, HttpStatusCode statusCode)
        {
            return new HttpClientResult<TSuccess, TError>
            {
                Error = error,
                StatusCode = statusCode
            };
        }
    }
}
