namespace Security.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Emissor { get; set; }
        public string ValidIn { get; set; }
    }
}
