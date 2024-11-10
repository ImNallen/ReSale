namespace ReSale.Domain.Activities;

public enum ActivityType
{
    /// <summary>
    /// An user is created.
    /// </summary>
    UserCreated,

    /// <summary>
    /// An customer is created.
    /// </summary>
    CustomerCreated,

    /// <summary>
    /// An customer is deleted.
    /// </summary>
    CustomerDeleted,

    /// <summary>
    /// An product is created.
    /// </summary>
    ProductCreated,

    /// <summary>
    /// An employee is created.
    /// </summary>
    EmployeeCreated,

    /// <summary>
    /// An order is created.
    /// </summary>
    OrderCreated,
}
