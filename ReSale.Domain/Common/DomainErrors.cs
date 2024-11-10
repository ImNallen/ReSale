namespace ReSale.Domain.Common;

public static class DomainErrors
{
    public static readonly Error InvalidCredentials = Error.Validation("Error.InvalidCredentials", "Invalid credentials.");

    public static Error Empty(string name) =>
        Error.Validation("Error.Empty", $"{name} can't be empty.");

    public static Error NotFound(string name) =>
        Error.NotFound("Error.NotFound", $"{name} not found.");

    public static Error Expired(string name) =>
        Error.Problem("Error.Expired", $"{name} has expired.");

    public static Error InvalidFormat(string name) =>
        Error.Validation("Error.InvalidFormat", $"Invalid format of {name}.");

    public static Error NotUnique(string name) =>
        Error.Conflict("Error.NotUnique", $"Provided {name} is not unique.");

    public static Error TooShort(string name) =>
        Error.Validation("Error.TooShort", $"{name} is too short.");

    public static Error Verified(string name) =>
        Error.Conflict("Error.Verified", $"{name} is already verified.");
}
