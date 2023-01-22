namespace Domain.Exceptions
{
    public class SourseDateException : BaseBadRequestException
    {
        public SourseDateException(string? message) : base(message)
        {
        }
    }
}
