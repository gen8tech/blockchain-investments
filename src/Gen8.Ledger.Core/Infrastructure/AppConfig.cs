namespace Gen8.Ledger.Core.Infrastructure
{
    public class AppConfig
    {
        public AppConfig()
        {
            // Set default value.
            MONGOLAB_URI = "mongodb://expense-point:E3^vuT3cNqm7@ds043615.mongolab.com:43615/expense-point";
        }
        public string MONGOLAB_URI { get; set; }
    }
}
