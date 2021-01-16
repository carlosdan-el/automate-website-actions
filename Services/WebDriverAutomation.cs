using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Edge.SeleniumTools;
using Xunit;
using OpenQA.Selenium;
using Services.Widgets;

namespace Services
{
    public class WebDriverAutomation
    {
        // read website file
        // create webdrive
        // start automation acess
        private string WebdriverOptions;
        private IWebDriver WebdriverBrowser = null;

        public void AutomationStart()
        {
            try
            {
                this.ReadWebsiteFiles();
                this.WebdriverBrowser.Quit();
            }
            catch(Exception error)
            {
                Assert.True(false, error.Message);
            }
        }

        private void ReadWebsiteFiles()
        {
            try
            {
                using(var sr = File.OpenText(@"F:/Repositories/automate-website-actions/Services/Config/websites.json"))
                {
                    List<Website> file = JsonConvert.DeserializeObject<List<Website>>(sr.ReadToEnd());

                    foreach(var website in file)
                    {
                        this.SetWebDriverBrowser(website.WebDriver);
                    }

                }
            }
            catch(Exception error)
            {
                Assert.True(false, error.Message);
            }

        }

        private void SetWebDriverBrowser(string webdriver)
        {
            webdriver.ToLower();

            switch(webdriver)
            {
                case "edge":
                var options = new EdgeOptions();
                options.UseChromium = true;
                options.AddExtension(@"F:/Repositories/automate-website-actions/Services/Extensions/MultiPass.crx");
                this.WebdriverBrowser = new EdgeDriver(@"F:/Repositories/automate-website-actions/Services/WebDrivers", options);
                    break;
                default:
                    this.WebdriverBrowser = new EdgeDriver(@"F:/Repositories/automate-website-actions/Services/WebDrivers");
                    break;
            }

        }

        private void Login(string type)
        {

        }

    }
}
