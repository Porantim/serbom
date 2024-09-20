namespace Serbom.Domain.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException() : base("Not found")
        {
        }
    }
}