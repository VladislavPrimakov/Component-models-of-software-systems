namespace Task6
{
    public class VaR
    {
        // Розрахунок VaR методом історичних симуляцій
        static public double CalculateVaRHistorical(Asset asset, double confidenceLevel)
        {
            List<double> assetReturns = new List<double>();

            for (int i = 1; i < asset.Prices.Count; i++)
            {
                double dailyReturn = (asset.Prices[i] - asset.Prices[i - 1]) / asset.Prices[i - 1];
                assetReturns.Add(dailyReturn);
            }

            assetReturns.Sort();
            int varIndex = (int)((1 - confidenceLevel) * assetReturns.Count);
            return assetReturns[varIndex];
        }
    }
}
