using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutomationPractical.UI.Models
{
    public class SearchCriteria
    {
        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}