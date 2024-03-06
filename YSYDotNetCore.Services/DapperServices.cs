using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSYDotNetCore.Services
{
    public class DapperServices
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        public DapperServices(SqlConnectionStringBuilder connectionStringBuilder)
        {
            _connectionStringBuilder = connectionStringBuilder;
        }

        public  IEnumerable<T> Query<T>(string query, object? param=null, CommandType commandType = CommandType.Text)
        {

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);

            return connection.Query<T>(query, param,commandType:CommandType.Text);

        }

        public int Execute(string query, object? param = null, CommandType commandType = CommandType.Text)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);

            return connection.Execute(query, param, commandType:commandType);
            
        }
    }
}
