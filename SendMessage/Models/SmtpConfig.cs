namespace SendMessage.Models
{
    public class SmtpConfig
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string EmailFrom { get; set; }
        public string Password { get; set; }
    }
}
