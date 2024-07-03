﻿namespace ReSale.Domain.Common;

public class PagedList<T>
{
    public List<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
    
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    
    public static PagedList<T> Create(
        List<T> items,
        int page, 
        int pageSize,
        int totalCount)
    {
        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}