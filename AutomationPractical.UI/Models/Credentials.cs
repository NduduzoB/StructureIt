using Newtonsoft.Json;

namespace AutomationPractical.UI.Models
{
    public static class Credentials
    {
        [JsonProperty("email")] public static string Email { get; } = "ndu@automationpractice.com";

        [JsonProperty("password")] public static string Password { get; } = "ndu@automationpractice.com";
    }
}