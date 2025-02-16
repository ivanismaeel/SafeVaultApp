using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SafeVaultApp.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(_connectionString));
        }

        public void SaveUserData(string username, string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (Username, Email) VALUES (@Username, @Email)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username ?? string.Empty);
                    command.Parameters.AddWithValue("@Email", email ?? string.Empty);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public bool LoginUser(string username, string password)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static string SanitizeInput(string input)
        {
            return input.Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public static bool IsValidXSSInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return !(input.ToLower().Contains("<script") || input.ToLower().Contains("<iframe"));
        }



    }
}
