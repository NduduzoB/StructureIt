using AutomationPractical.UI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace AutomationPractical.UI.Util
{
    public class TestDataReader
    {
        public static List<SearchCriteria> GetTestData()
        {
            var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("TestData.json").Build();
            var section = config.GetSection(nameof(SearchCriteria));
            var example = section.Get<List<SearchCriteria>>();
            return example;
        }
    }
}