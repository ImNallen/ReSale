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
            .Map(dest => dest.ShippingStreet, src => src.ShippingAddress.Street)
            .Map(dest => dest.ShippingCity, src => src.ShippingAddress.City)
            .Map(dest => dest.ShippingZipCode, src => src.ShippingAddress.ZipCode)
            .Map(dest => dest.ShippingCountry, src => src.ShippingAddress.Country)
            .Map(dest => dest.ShippingState, src => src.ShippingAddress.State)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber.Value)
            .Map(dest => dest.BillingStreet, src => src.BillingAddress!.Street)
            .Map(dest => dest.BillingCity, src => src.BillingAddress!.City)
            .Map(dest => dest.BillingZipCode, src => src.BillingAddress!.ZipCode)
            .Map(dest => dest.BillingCountry, src => src.BillingAddress!.Country)
            .Map(dest => dest.BillingState, src => src.BillingAddress!.State);
    }
}