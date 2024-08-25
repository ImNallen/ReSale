namespace ReSale.Domain.Common;

/// <summary>
/// Represents the type of error that occurred.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Failure error type.
    /// </summary>
    Failure = 0,

    /// <summary>
    /// Validation error type.
    /// </summary>
    Validation = 1,

    /// <summary>
    /// Problem error type.
    /// </summary>
    Problem = 2,

    /// <summary>
    /// Not found error type.
    /// </summary>
    NotFound = 3,

    /// <summary>
    /// Conflict error type.
    /// </summary>
    Conflict = 4
}
