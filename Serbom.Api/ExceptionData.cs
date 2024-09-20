namespace Serbom.Api
{
    public class ExceptionData
    {
        public ExceptionData(string message)
        {
            this.message = message;
        }

        public string? message { get; set; }
    }
}