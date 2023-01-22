namespace Domain.Exceptions
{
    public class UnknownOperationTypeException : BaseBadRequestException
    {
        public UnknownOperationTypeException(string? message) : base(message)
        {
        }
    }
}
