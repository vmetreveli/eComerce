namespace Discount.Domain.Exceptions;

public class PersonRelationAlreadyExistsException : ApplicationException
{
    public PersonRelationAlreadyExistsException(string title, string message) :
        base(title, message)
    {
    }
}