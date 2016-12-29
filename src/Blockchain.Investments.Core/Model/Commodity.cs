namespace Blockchain.Investments.Core.Model
{
    public class Commodity : IEntity
    {
        private string id = string.Empty;
        public string Title {get; set;}
        public string Ticker {get;set;}
        public CommodityType Type {get;set;}
        public string ImageUrl {get;set;}
        public string UniqueId
        {
            get
            {
                if (!this.Id.ToString().Equals("000000000000000000000000"))
                    id = this.Id.ToString();

                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}
