namespace Catalog.Domain.Exceptions;

public class PersonRelationNotFoundException : NotFoundException
{
    public PersonRelationNotFoundException() : base(nameof(PersonRelationNotFoundException))
    {
    }
}