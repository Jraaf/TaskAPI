namespace Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base() { }
    public NotFoundException(string message)
        : base(message) { }
    public NotFoundException(Guid id)
        : base($"there is no such object with {id} id") { }
}
