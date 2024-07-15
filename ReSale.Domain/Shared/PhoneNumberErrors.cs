using ReSale.Domain.Common;

namespace ReSale.Domain.Shared;

public static class PhoneNumberErrors
{
    public static readonly Error Empty = Error.Problem(
        "PhoneNumber.Empty", 
        "PhoneNumber is empty");
    
    public static readonly Error NotValid = Error.Problem(
        "PhoneNumber.NotValid", 
        "PhoneNumber is not a valid phone number");
}