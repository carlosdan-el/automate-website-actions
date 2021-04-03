using System;
using System.IO;
using Domain.common;
using Domain.Services;
using Domain.validations;

namespace Presentation
{
    public class Automation
    {
        private string configFilePath = "";
        private string webdriverFilePath = "";

        public Automation(string configPath, string webDriverPath)
        {
            SetConfigFilePath(configPath);
            SetWebdriverPath(webDriverPath);
        }

        public void Start()
        {
            try
            {
                var webdriver = new WebDriver(configFilePath, webdriverFilePath);
                webdriver.Open();
            }
            catch(Exception error)
            {
                Console.WriteLine($"{error.Message}");
            }
        }

        private void SetConfigFilePath(string filePath)
        {
            ConsoleMessage.Print("Start file validation.");
            FileValidation.FileExists(filePath);
            configFilePath = filePath;
            ConsoleMessage.Print("File validation finished with success.", "success");
        }

        private void SetWebdriverPath(string filePath)
        {
            ConsoleMessage.Print("Start webdriver validation.");
            webdriverFilePath = filePath;
            ConsoleMessage.Print("File validation finished with success.", "success");
        }

    }
}
