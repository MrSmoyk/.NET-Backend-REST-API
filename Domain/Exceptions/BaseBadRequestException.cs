namespace Domain.Exceptions
{
    public class BaseBadRequestException : Exception
    {
        public BaseBadRequestException(string? message) : base(message)
        {
        }
    }
}
