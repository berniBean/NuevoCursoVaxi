using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConexionConfiguracion> _configs;

        public FactoryConnection(IOptions<ConexionConfiguracion> configs)
        {
            _configs = configs;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null)
                _connection = new MySqlConnection(_configs.Value.DefaultConnection);

            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            return _connection;
        }
    }
}
