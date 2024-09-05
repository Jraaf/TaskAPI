namespace Common.Exceptions;

public class CredentialsExistException : Exception
{
    public CredentialsExistException(string credential)
        : base($"{credential} is already taken.")
    { }

}
