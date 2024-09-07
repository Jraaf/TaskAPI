namespace Common.Exceptions;

public class ForbiddenActionException : Exception
{
    public ForbiddenActionException(Guid id)
        : base($"You dont have access to {id} task.")
    { }
}
