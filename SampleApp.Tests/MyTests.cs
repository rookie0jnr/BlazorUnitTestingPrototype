using Microsoft.AspNetCore.Components.Testing;
using SampleApp.Pages;
using System;
using Xunit;

namespace SampleApp.Tests
{
    public class MyTests
    {
        private TestHost host = new TestHost();

        [Fact]
        public void CounterWorks()
        {
            var component = host.AddComponent<Counter>();
            //var buttonIncrement = component.Find("button.inc");

            //int i = 5;
            Func<string> countValue = () => component.Find("#count").InnerText;

            Assert.Equal("Counter", component.Find("h1").InnerText);
            Assert.Equal("Current count: 0", countValue());

            component.Find("button.inc").Click();
            Assert.Contains("Current count: 1", countValue());

            component.Find("button.dec").Click();
            Assert.Contains("Current count: 0", countValue());
        }
    }
}
