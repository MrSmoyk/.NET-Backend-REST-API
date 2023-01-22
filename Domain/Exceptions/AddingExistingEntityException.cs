namespace Domain.Exceptions
{
    public class AddingExistingEntityException : BaseBadRequestException
    {
        public AddingExistingEntityException(string? message) : base(message)
        {
        }
    }
}
