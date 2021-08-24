namespace WebApi.Models
{
    public class ApiResponse
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        
        

        public ApiResponse()
        {
            Success = 0;
        }
    }
}