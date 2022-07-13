namespace AutomationPractical.UI.Util
{
    public static class XPathFinder
    {
        public static string FindXPath(string category)
        {
            return category switch
            {
                Constants.Tops or Constants.Dresses => "//a[contains(text(),'Women')]",
                Constants.CasualDresses or Constants.EveningDresses or Constants.SummerDresses => "(//a[contains(text(),'Dresses')])[5]",
                _ => "(//a[contains(text(),'T-shirts')])[2]"
            };
        }
    }
}