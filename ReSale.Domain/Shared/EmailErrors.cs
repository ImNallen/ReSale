using ReSale.Domain.Common;

namespace ReSale.Domain.Shared;

public static class EmailErrors
{
    public static readonly Error Empty = Error.Problem("Email.Empty", "Email is empty");

    public static readonly Error InvalidFormat = Error.Problem(
        "Email.InvalidFormat", "Email format is invalid");
    
    public static readonly Error NotUnique = Error.Problem("Email.NotUnique", "Provided email is not unique");
}
