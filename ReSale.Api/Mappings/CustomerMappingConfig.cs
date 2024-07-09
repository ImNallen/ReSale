using Mapster;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Application.Customers.Results;

namespace ReSale.Api.Mappings;

public class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CustomerResult, CustomerResponse>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Email, s => s.Email)
            .Map(d => d.FirstName, s => s.FirstName)
            .Map(d => d.LastName, s => s.LastName)
            .Map(d => d.Street, s => s.Street)
            .Map(d => d.City, s => s.City)
            .Map(d => d.ZipCode, s => s.ZipCode)
            .Map(d => d.Country, s => s.Country)
            .Map(d => d.State, s => s.State);
    }
}