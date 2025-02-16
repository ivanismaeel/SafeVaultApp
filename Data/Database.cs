using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace SafeVaultApp.Data
{
    public class Database
    {
        private readonly string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public bool LoginUser(string username, string password)
        {
            string allowedSpecialCharacters = "!@#$%^&*?";
            if (!ValidationHelpers.IsValidInput(username) || !ValidationHelpers.IsValidInput(password, allowedSpecialCharacters)) return false;

            string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password";

            using (var connection = new SqlConnection(connectionString))
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

        public void AddUser(string username, string email)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users (Username, Email) VALUES (@Username, @Email)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Failed to save user data", ex);
            }
        }

        public string SanitizeInput(string input)
        {
            return input.Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}
