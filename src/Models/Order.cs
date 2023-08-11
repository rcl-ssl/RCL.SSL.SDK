#nullable disable

namespace RCL.SSL.SDK
{
    public class Order
    {
        public string status { get; set; }
        public List<ValidationToken> validationTokens { get; set; }
        public string orderUri { get; set; }
    }

    public class ValidationToken
    {
        public string tokenName { get; set; }
        public string tokenValue { get; set; }
        public string challengeType { get; set; }
        public string domain { get; set; }
    }

    public class Challenge
    {
        public string challengeType { get; set; }
        public string status { get; set; }
        public string token { get; set; }
    }
}
