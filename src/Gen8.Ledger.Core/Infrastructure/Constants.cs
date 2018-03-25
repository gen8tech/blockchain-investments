namespace Gen8.Ledger.Core.Infrastructure
{
    public static class Constants
    {
        public const string ApplicationName = "Gen8 Tech Ledger";
        public const string DatabaseName = "expense-point";
        public const string EventStoreCollectionName = "EventStore";
        public const string AuthorizationPolicy = "BitcoinUser";
        public const string ClaimType = "BitId";
        public const string ClaimValue = "PublicAddress";
        public const int SpanTimeInSeconds = 600;
    }
}
