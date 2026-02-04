namespace PasswordManager
{
    public static class AppState
    {
        public static string MasterPassword { get; set; }

        public static void ClearSession()
        {
            MasterPassword = null;
        }
    }
}