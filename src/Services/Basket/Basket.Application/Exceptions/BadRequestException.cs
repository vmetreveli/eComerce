using System;

namespace Basket.Application.Exceptions;

public abstract class BadRequestException : ApplicationException
{
    public BadRequestException(string message, object key)
        : base($"Bad Request, {message}")
    {
    }
}