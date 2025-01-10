using System.Text.RegularExpressions;

namespace back_end.Services;

public class PasswordService : IPasswordService
{
    public string ValidatePassword(string password)
    {
        // Password must be between 7-14 characters in length
        if (string.IsNullOrEmpty(password) || password.Length > 14 || password.Length < 7)
            return "Password must be between 7-14 characters in length";

        // Password does not contain special characters other than !£$^*#
        if (!ContainsOnlyAllowedSpecialCharacters(password))
            return "Password does not contain special characters other than !£$^*#";

        // Password must contain at least 1 number and one special characters
        if (!ContainsOneNumberAndOneSpecialCharacters(password))
            return "Password must contain at least 1 number and one special characters";

        return "";
    }
    
    public bool IsPasswordCommon(string? password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        string filePath = Path.Combine(Environment.CurrentDirectory, "Data", "common-passwords.txt");
        //todo: optimisation is required here, some passwords dont meet our criteria anyway.
        using (StreamReader reader = new StreamReader(filePath))
        {
            if (reader == null) return false;

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if(string.IsNullOrEmpty(line) && line.Equals(password, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
        }

        return false;
    }

    private bool ContainsOnlyAllowedSpecialCharacters(string input)
    {
        // Regular expression to match allowed characters
        string pattern = @"^[a-zA-Z0-9!£$^*#]*$";
        return Regex.IsMatch(input, pattern);
    }

    private bool ContainsOneNumberAndOneSpecialCharacters(string input)
    {
        string pattern = @"^(?=.*\d)(?=.*[!@#$%^&*(),.?"":{ }|<>]).+$";

        // Check if the input matches the pattern
        return Regex.IsMatch(input, pattern);
    }
}