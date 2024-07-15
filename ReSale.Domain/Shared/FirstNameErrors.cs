using ReSale.Domain.Common;

namespace ReSale.Domain.Shared;

public static class FirstNameErrors
{
    public static readonly Error Empty = Error.Problem("FirstName.Empty", "FirstName is empty");
}