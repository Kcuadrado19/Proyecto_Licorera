using Proyecto_Licorera_Corchos.web.Core.Pagination;

namespace Proyecto_Licorera_Corchos.web.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationRequest request)
        {
            return query.Skip((request.Page - 1) * request.RecordPerPage)
                .Take(request.RecordPerPage);

        }


    }
}
