namespace Discount.Domain.Exceptions;

public class PersonRelationNotFoundException : NotFoundException
{
    public PersonRelationNotFoundException() : base(nameof(PersonRelationNotFoundException))
    {
    }
}