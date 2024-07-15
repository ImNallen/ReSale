using Mapster;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Customers;

namespace ReSale.Application.Mappings;

public class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, CustomerResult>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Email, src => src.Email.Value)
            .Map(dest => dest.FirstName, src => src.FirstName.Value)
            .Map(dest => dest.LastName, src => src.LastName.Value)
            .Map(dest => dest.Street, src => src.Address.Street)
            .Map(dest => dest.City, src => src.Address.City)
            .Map(dest => dest.ZipCode, src => src.Address.ZipCode)
            .Map(dest => dest.Country, src => src.Address.Country)
            .Map(dest => dest.State, src => src.Address.State)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber.Value);
    }
}