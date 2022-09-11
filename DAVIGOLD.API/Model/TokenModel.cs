namespace DAVIGOLD.Model
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime Expires { get; set; }
    }
}
