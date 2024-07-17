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
            .Map(d => d.ShippingStreet, s => s.ShippingStreet)
            .Map(d => d.ShippingCity, s => s.ShippingCity)
            .Map(d => d.ShippingZipCode, s => s.ShippingZipCode)
            .Map(d => d.ShippingCountry, s => s.ShippingCountry)
            .Map(d => d.ShippingState, s => s.ShippingState)
            .Map(d => d.BillingStreet, s => s.BillingStreet)
            .Map(d => d.PhoneNumber, s => s.PhoneNumber)
            .Map(d => d.BillingCity, s => s.BillingCity)
            .Map(d => d.BillingZipCode, s => s.BillingZipCode)
            .Map(d => d.BillingCountry, s => s.BillingCountry)
            .Map(d => d.BillingState, s => s.BillingState);
    }
}