using Newtonsoft.Json.Linq;

namespace Cursos.Helper
{
    public class ErrorHelper
    {
        public static ResponseObject Response(int StatusCode, string Message)
        {
            return new ResponseObject(){
                StatusCode = StatusCode,
                Message = Message
            };
        }       
    }

    public class ResponseObject 
    {
        public int StatusCode {get; set;}
        public string Message {get; set;}
    }
}