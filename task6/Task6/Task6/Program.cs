using Task6;

// Визначення нечітких множин для дохідності
var lowReturn = new FuzzySet("Low", 0, 0.02, 0.04);
var mediumReturn = new FuzzySet("Medium", 0.03, 0.05, 0.07);
var highReturn = new FuzzySet("High", 0.06, 0.08, 0.10);

// Визначення нечітких множин для ризику
var lowRisk = new FuzzySet("Low", 0, 0.01, 0.02);
var mediumRisk = new FuzzySet("Medium", 0.015, 0.03, 0.045);
var highRisk = new FuzzySet("High", 0.04, 0.06, 0.10);

// Визначення нечітких множин для привабливості
var lowAttractiveness = new FuzzySet("Low", 2, 3, 5);
var mediumAttractiveness = new FuzzySet("Medium", 8, 9, 13);
var highAttractiveness = new FuzzySet("High", 14, 16, 18);

List<Asset> assetsF = Asset.GetAssets();

// Оцінка привабливості кожного активу
foreach (var asset in assetsF)
{
    // Визначення членства дохідності в нечітких множинах:
    double lowReturnMembership = lowReturn.Membership(asset.Return);
    double mediumReturnMembership = mediumReturn.Membership(asset.Return);
    double highReturnMembership = highReturn.Membership(asset.Return);

    // Визначення членства ризику в нечітких множинах:
    double lowRiskMembership = lowRisk.Membership(asset.Risk);
    double mediumRiskMembership = mediumRisk.Membership(asset.Risk);
    double highRiskMembership = highRisk.Membership(asset.Risk);

    // Далі обчислюються значення привабливості для кожного активу, використовуючи мінімальні значення членства в нечітких множинах прибутковості та ризику:
    double lowAttractivenessValue = Math.Min(lowReturnMembership, lowRiskMembership);
    double mediumAttractivenessValue = Math.Min(mediumReturnMembership, mediumRiskMembership);
    double highAttractivenessValue = Math.Min(highReturnMembership, lowRiskMembership);

    // Обчислення середньозваженої привабливості:
    double numerator = lowAttractivenessValue * lowAttractiveness.Peak +
                       mediumAttractivenessValue * mediumAttractiveness.Peak +
                       highAttractivenessValue * highAttractiveness.Peak;
    double denominator = lowAttractivenessValue + mediumAttractivenessValue + highAttractivenessValue;
    asset.Attractiveness = numerator / denominator;
}

// Сортування активів за привабливістю
assetsF = assetsF.OrderByDescending(a => a.Attractiveness).ToList();

// Виведення результату
Console.WriteLine("Optimal Portfolio:");
foreach (var asset in assetsF)
{
    Console.WriteLine($"{asset.Name}: Return = {asset.Return:F4}, Risk = {asset.Risk:F4}, Attractiveness = {asset.Attractiveness:F4}");
}


List<Asset> assetsV = Asset.GetAssets();
// Розрахунок VaR для кожного активу та всього портфеля
double confidenceLevel = 0.9;
foreach (var asset in assetsV)
{
    double varHistorical = VaR.CalculateVaRHistorical(asset, confidenceLevel);

    Console.WriteLine($"90% VaR for {asset.Name} (Historical): {varHistorical:F4}");
}

Console.ReadKey();
