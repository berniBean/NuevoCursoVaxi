using persistencia.DTO;

namespace persistencia.DapperConexion.Paginacion
{
    public interface IPaginacion
    {
        Task<PaginacionModel> DevolverPaginacion(                                             
            string storePocedure,                                             
            int numeroPagina,                                              
            int cantidadElementos,                                  
            IDictionary<string,object>ParametrosFiltro,
            int ordenamientoColumna);
    }
}
