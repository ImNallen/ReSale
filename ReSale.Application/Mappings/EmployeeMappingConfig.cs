using Mapster;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Employees;

namespace ReSale.Application.Mappings;

public class EmployeeMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Employee, EmployeeResult>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Email, s => s.Email.Value)
            .Map(d => d.FirstName, s => s.FirstName.Value)
            .Map(d => d.LastName, s => s.LastName.Value);
    }
}