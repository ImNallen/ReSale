﻿using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Requests.Customers;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Create;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Customers;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/customers", async (
                CreateCustomerRequest request,
                ISender sender,
                IMapper mapper,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateCustomerCommand(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.ShippingStreet,
                    request.ShippingCity,
                    request.ShippingState,
                    request.ShippingZipCode,
                    request.ShippingCountry,
                    request.PhoneNumber,
                    request.BillingStreet,
                    request.BillingCity,
                    request.BillingZipCode,
                    request.BillingCountry,
                    request.BillingState);

                Result<CustomerResult> result = await sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }

                return Results.Ok(mapper.Map<CustomerResponse>(result.Value));
            })
            .WithTags(Tags.Customers)
            .WithDescription("Creates a new customer.")
            .WithName("Create Customer")
            .WithSummary("Create Customer")
            .Produces(StatusCodes.Status200OK, typeof(CustomerResponse))
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();
}
