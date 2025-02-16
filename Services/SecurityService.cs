using System.Text.RegularExpressions;

namespace SafeVaultApp.Services
{
    public class SecurityService
    {
        // Implement method to check for potential SQL injection vulnerabilities
        public bool ValidateInput(string input)
        {
            // Regular expression to detect common SQL injection patterns
            var sqlInjectionPattern = @"('|--|;|--|\bSELECT\b|\bINSERT\b|\bUPDATE\b|\bDELETE\b|\bDROP\b|\bALTER\b|\bCREATE\b|\bEXEC\b|\bUNION\b)";
            var match = Regex.IsMatch(input, sqlInjectionPattern, RegexOptions.IgnoreCase);

            return !match;
        }

        // Validate user input for length and unwanted characters
        public bool ValidateUserInput(string input)
        {
            // Example: Validate for length and alphanumeric characters
            if (input.Length < 3 || input.Length > 50) return false;
            return Regex.IsMatch(input, @"^[a-zA-Z0-9]*$");
        }

        // Ensure password hashing and salting
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
