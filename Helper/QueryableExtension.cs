using PeliculasAPI_Udemy.DTOs;

namespace PeliculasAPI_Udemy.Helper
{
    public static class QueryableExtension
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacion)
        {
            return queryable.Skip((paginacion.Page - 1) * paginacion.RecordsPerPage).Take(paginacion.RecordsPerPage);
        }
    }
}
