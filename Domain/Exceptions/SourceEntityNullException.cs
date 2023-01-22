namespace Domain.Exceptions
{
    public class SourceEntityNullException : BaseBadRequestException
    {
        public SourceEntityNullException(string? message) : base(message)
        {
        }
    }
}
