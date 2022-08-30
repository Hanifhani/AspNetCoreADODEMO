namespace DAVIGOLD.API.Model
{
    public class AppSettingModel
    {
        public string? ConnectionString { get; set; }
        public JWT JWT { get; set; }
    }

    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
