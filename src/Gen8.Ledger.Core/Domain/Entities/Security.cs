using Gen8.Ledger.Core.Infrastructure.Domain;

namespace Gen8.Ledger.Core.Domain
{
    public class Security : BaseEntity
    {
        public string Title {get; set;}
        public string Ticker {get;set;}
        public string Code {get;set;}
        public int Fraction {get;set;}
        public MarketType Type {get;set;}
        public PricingMechanism Pricing {get;set;}

        public string ImageUrl {get;set;}
        public string DetailsUrl {get;set;}
        public string Description {get; set;}
        public string Namespace {get;set;}
        public string QuoteSource {get;set;}
    }
    public enum MarketType
    {
           ForexCurrency = 1,
           CryptoCurrency = 2,
           Metal = 3,
           Stock = 4,
           Bond = 5,
           ETF = 6,
           Commodity = 7,
           Indice = 8
    }
    public enum PricingMechanism
    {
           Historical = 1,
           Market = 2
    }
}
