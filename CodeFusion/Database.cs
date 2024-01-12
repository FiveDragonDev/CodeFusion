using System.Data;
using System.Data.SqlClient;

namespace CodeFusion
{
    public class Database
    {
        public string? ConnectionString { private get; set; }

        public Database(string connectionString) => ConnectionString = connectionString;

        public DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlCommand command = new(query, connection);
            if (parameters != null)
                command.Parameters.AddRange(parameters);
            SqlDataAdapter adapter = new(command);
            DataTable dataTable = new();
            adapter.Fill(dataTable);
            return dataTable;
        }
        public int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlCommand command = new(query, connection);
            if (parameters != null)
                command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }
        public object ExecuteScalar(string query, SqlParameter[]? parameters = null)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlCommand command = new(query, connection);
            if (parameters != null)
                command.Parameters.AddRange(parameters);
            return command.ExecuteScalar();
        }
        public void BeginTransaction()
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
        }
        public void CommitTransaction()
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            transaction.Commit();
        }
        public void RollbackTransaction()
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            transaction.Rollback();
        }
    }
}
