﻿using System;

namespace Discount.Domain.Exceptions;

public sealed class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(Guid? userId)
        : base($"The user with the identifier {userId} was not found.")
    {
    }
}