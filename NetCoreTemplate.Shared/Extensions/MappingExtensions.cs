using NetCoreTemplate.Shared.Models;

namespace NetCoreTemplate.Shared.Extensions;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, PaginationRequest req)
        => PaginatedList<TDestination>.CreateAsync(queryable, req.PageNumber, req.PageSize);
}