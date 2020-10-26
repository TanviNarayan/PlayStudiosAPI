namespace PlayStudios.Model
{
    public class LoginData
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class ResetPassSendEmailData
    {
        public string email { get; set; }
        public string url { get; set; }
    }

    public class ResetPassData
    {
        public string token { get; set; }
        public string password { get; set; }
    }
}
