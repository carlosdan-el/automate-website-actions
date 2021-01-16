using System;
using Services;
using Xunit;

namespace Tests
{
    public class AutomateWebsiteActions
    {
        [Fact]
        public void StartAcessAutomation()
        {
            WebDriverAutomation webDriver = new WebDriverAutomation();
            webDriver.AutomationStart();
        }
    }
}
