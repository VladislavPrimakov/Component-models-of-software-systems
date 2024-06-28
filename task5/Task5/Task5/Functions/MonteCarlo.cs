using Task5.Models;

namespace Task5.Functions
{
    public class MonteCarlo
    {
        public static double Run(Project project, int simulations, double confidence) {
            List<double> outcomes = new List<double>();
            var random = new Random();
            // simulation
            for (int i = 0; i < simulations; i++) {
                double sumCost = project.BaseCost;
                foreach (var risk in project.Risks) {
                    if (random.NextDouble() < risk.Probability) {
                        sumCost += risk.Impact;
                    }
                }
                outcomes.Add(sumCost);
            }
            // calculate VaR
            outcomes.Sort();
            int index = (int)((1.0 - confidence) * outcomes.Count);
            double expectedChange = outcomes.Average();
            double worstExpectedChange = outcomes[index];
            return Math.Abs(expectedChange - worstExpectedChange);
        }
    }
}
