using ReSale.Domain.Common;

namespace ReSale.Domain.Shared;

public static class LastNameErrors
{
    public static readonly Error Empty = Error.Problem("LastName.Empty", "LastName is empty");
}