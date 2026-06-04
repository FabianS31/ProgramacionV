using Microsoft.Data.SqlClient;

namespace ProgramacionV.Models
{
    public class DBController
    {
        private readonly IConfiguration _configuration;

        public DBController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public SqlConnection GetConnection()
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            return new SqlConnection(connectionString);
        }

        public int Count(string tableName)
        {
            int total = 0;

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string query = $"SELECT COUNT(*) FROM {tableName}";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    total = (int)cmd.ExecuteScalar();
                }
            }

            return total;
        }
    }
}