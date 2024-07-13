﻿using Microsoft.AspNetCore.Components;
using Refit;
using ReSale.Api.Contracts.Requests.Customers;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Web.Models;

namespace ReSale.Web.Clients;

public interface ICustomerClient
{
    [Get("/api/v1/customers/search?searchTerm={searchTerm}&page={page}&pageSize={pageSize}")]
    Task<PagedList<CustomerResponse>> SearchCustomers(
        [Query] string? searchTerm,
        [Query] int page,
        [Query] int pageSize);
    
    [Delete("/api/v1/customers/{id}")]
    Task DeleteCustomer(Guid id);
    
    [Post("/api/v1/customers")]
    Task<CustomerResponse> CreateCustomer(CreateCustomerRequest customerRequest);
}