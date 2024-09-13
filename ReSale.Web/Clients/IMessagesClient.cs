using System;
using Refit;
using ReSale.Api.Contracts.Responses.Messages;
using ReSale.Web.Models;

namespace ReSale.Web.Clients;

public interface IMessagesClient
{
    [Get("/api/v1/messages?sortColumn={sortColumn}&sortOrder={sortOrder}&page={page}&pageSize={pageSize}")]
    Task<PagedList<MessageResponse>> Get(
        [Query] string? sortColumn,
        [Query] string? sortOrder,
        [Query] int page,
        [Query] int pageSize);
}
