using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace YSYDotNetCore.Services
{
    public class AdoDotNetService
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        public AdoDotNetService(SqlConnectionStringBuilder connectionStringBuilder)
        {
            _connectionStringBuilder = connectionStringBuilder;
        }
        public DataTable Query(string query,CommandType commandType=CommandType.Text,params SqlParameter[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = commandType;
            command.Parameters.AddRange(sqlParameters);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return dt;
        }

        public List<T> Query<T>(string query, CommandType commandType = CommandType.Text, params SqlParameter[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = commandType;
            command.Parameters.AddRange(sqlParameters);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            var jsonstr=JsonConvert.SerializeObject(dt);

            return JsonConvert.DeserializeObject<List<T>>(jsonstr)!;
        }

        public int Execute(string query, CommandType commandType = CommandType.Text, params SqlParameter[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddRange(sqlParameters);
            int result = command.ExecuteNonQuery();
            connection.Close();
            return result;

        }
    }
}
