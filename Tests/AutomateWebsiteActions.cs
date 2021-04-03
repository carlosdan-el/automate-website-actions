using System;
using Xunit;
using Domain.Services;

namespace Tests
{
    public class AutomateWebsiteActions
    {
        [Fact]
        public void StartAcessAutomation()
        {
            try
            {
                Webdriver web = new Webdriver("./", "./");
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
