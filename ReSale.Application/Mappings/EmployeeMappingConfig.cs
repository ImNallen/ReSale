using Mapster;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Employees;

namespace ReSale.Application.Mappings;

public class EmployeeMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<Employee, EmployeeResult>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Email, s => s.Email.Value)
            .Map(d => d.FirstName, s => s.FirstName.Value)
            .Map(d => d.LastName, s => s.LastName.Value)
            .Map(d => d.HireDate, s => s.HireDate)
            .Map(d => d.Street, s => s.Address.Street)
            .Map(d => d.City, s => s.Address.City)
            .Map(d => d.ZipCode, s => s.Address.ZipCode)
            .Map(d => d.Country, s => s.Address.Country)
            .Map(d => d.State, s => s.Address.State)
            .Map(d => d.Amount, s => s.Salary.Amount)
            .Map(d => d.Currency, s => s.Salary.Currency.Code);
}
