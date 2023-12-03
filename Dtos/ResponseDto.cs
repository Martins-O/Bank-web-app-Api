using System.Net;

namespace BankAppWebApi.Dtos
{
    public class ResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseDto(HttpStatusCode S, string m, object d)
        {
            this.StatusCode = S;
            this.Data = d;
            this.Message = m;
        }
        public ResponseDto(HttpStatusCode S, string m)
        {
            this.StatusCode = S;
            this.Message = m;
        }
    }
}
