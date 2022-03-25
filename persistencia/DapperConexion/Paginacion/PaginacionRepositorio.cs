using Dapper;
using persistencia.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistencia.DapperConexion.Paginacion
{
    public class PaginacionRepositorio : IPaginacion
    {
        private readonly IFactoryConnection _factoryConnection;
        public PaginacionRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public  Task<PaginacionModel> DevolverPaginacion(string storePocedure, int numeroPagina, int cantidadElementos, IDictionary<string, object> ParametrosFiltro, int ordenamientoColumna)
        {
            throw new NotImplementedException();
            //PaginacionModel pagModel = new PaginacionModel();
            //List<IDictionary<string, object>> ListaReporte = null;
            //try
            //{
            //    using (var connection = _factoryConnection.GetConnection())
            //    {

            //        DynamicParameters parametros = new DynamicParameters();
            //        foreach (var item in ParametrosFiltro)
            //        {
            //            parametros.Add("@" + item.Key, item.Value);
            //        }

            //        parametros.Add("@NumeroPagina", numeroPagina);
            //        parametros.Add("@CantidadElementos", cantidadElementos);
            //        parametros.Add("@Ordenamiento", ordenamientoColumna);

                    

            //        var result = await connection.QueryAsync(storePocedure, null, commandType: CommandType.StoredProcedure);
            //        ListaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
            //        pagModel.ListaRecords = ListaReporte;

            //    }
            //}
            //catch (Exception)
            //{

            //    throw new Exception("No se pudo ejecutar el procedimiento almacenado");
            //}
            //finally
            //{
            //    _factoryConnection.CloseConnection();
            //}
        }
    }
}
