namespace Task6
{
    public class Asset
    {
        public string Name { get; set; }
        public List<double> Prices { get; set; }
        public double Return { get; set; }
        public double Risk { get; set; }
        public double Attractiveness { get; set; }

        public Asset(string name, List<double> prices)
        {
            Name = name;
            Prices = prices;
            CalculateReturnAndRisk();
        }

        // обчислює стандартне відхилення (ризик) для списку доходностей активу returns
        private void CalculateReturnAndRisk()
        {
            List<double> returns = new List<double>();
            for (int i = 1; i < Prices.Count; i++)
            {
                double dailyReturn = (Prices[i] - Prices[i - 1]) / Prices[i - 1];
                returns.Add(dailyReturn);
            }

            Return = returns.Average();
            Risk = Math.Sqrt(returns.Average(v => Math.Pow(v - Return, 2)));
        }

        public static List<Asset> GetAssets()
        {
            List<Asset> assets = new List<Asset>
            {
                new Asset("Asset1", new List<double> { 100, 102, 101, 103, 105, 104, 106, 108, 107, 109 }),
                new Asset("Asset2", new List<double> { 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 }),
            };
            return assets;
        }
    }
}
