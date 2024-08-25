using Mapster;
using ReSale.Application.Products.Results;
using ReSale.Domain.Products;

namespace ReSale.Application.Mappings;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<Product, ProductResult>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Name, s => s.Name.Value)
            .Map(d => d.Description, s => s.Description.Value)
            .Map(d => d.Price, s => s.Price.Amount)
            .Map(d => d.Currency, s => s.Price.Currency.Code);
}
