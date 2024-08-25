using Mapster;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Customers;

namespace ReSale.Application.Mappings;

public class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<Customer, CustomerResult>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Email, s => s.Email.Value)
            .Map(d => d.FirstName, s => s.FirstName.Value)
            .Map(d => d.LastName, s => s.LastName.Value)
            .Map(d => d.ShippingStreet, s => s.ShippingAddress.Street)
            .Map(d => d.ShippingCity, s => s.ShippingAddress.City)
            .Map(d => d.ShippingZipCode, s => s.ShippingAddress.ZipCode)
            .Map(d => d.ShippingCountry, s => s.ShippingAddress.Country)
            .Map(d => d.ShippingState, s => s.ShippingAddress.State)
            .Map(d => d.PhoneNumber, s => s.PhoneNumber.Value)
            .Map(d => d.BillingStreet, s => s.BillingAddress!.Street)
            .Map(d => d.BillingCity, s => s.BillingAddress!.City)
            .Map(d => d.BillingZipCode, s => s.BillingAddress!.ZipCode)
            .Map(d => d.BillingCountry, s => s.BillingAddress!.Country)
            .Map(d => d.BillingState, s => s.BillingAddress!.State);
}
