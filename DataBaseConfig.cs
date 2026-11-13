using MySqlConnector;

namespace CyberSafetyChatbotGUI.Data
{
    public static class DataBaseConfig
    {
        public static string? ConnectionString =>
            Environment.GetEnvironmentVariable("CYBERSAFETY_MYSQL_CONNECTION");

        public static bool IsConfigured => !string.IsNullOrWhiteSpace(ConnectionString);

        public static bool TryOpenConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!IsConfigured)
            {
                errorMessage = "MySQL connection is not configured.";
                return false;
            }

            try
            {
                using MySqlConnection connection = new(ConnectionString!);
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
