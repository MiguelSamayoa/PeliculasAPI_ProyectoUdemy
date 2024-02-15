using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI_Udemy.Helper
{
    public static class HttpContextExtencion
    {
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext, IQueryable<T> queryable, int cantidadRegistrosPorPagina)
        {
            double conteo = await queryable.CountAsync();
            double totalPaginas = Math.Ceiling(conteo / cantidadRegistrosPorPagina);
            httpContext.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
        }
    }
}
