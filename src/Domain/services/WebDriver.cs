using System;
using OpenQA.Selenium;
using Microsoft.Edge.SeleniumTools;
using System.IO;
using System.Text.Json;
using OpenQA.Selenium.Support.UI;
using Domain.Entities;
using Domain.Validations;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Services
{
    public class WebDriver
    {
        protected JsonFile settings;
        protected IWebDriver webDriver;
        protected WebDriverWait wait;

        public WebDriver(string settingsFilePath, string webDriverPath, int webDriverWaitTime = 100)
        {
                SetWebDriverSettings(settingsFilePath);
                SetWebDriver(settings.webdriver, webDriverPath);
                wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(webDriverWaitTime));
        }

        private void SetWebDriverSettings(string filePath)
        {
            ConsoleMessage.Print("Read config file and applying settings.");
            
            FileValidation.ExtensionIsValid(filePath, ".json");

            string file = File.ReadAllText(filePath);
            JsonFile data = JsonSerializer.Deserialize<JsonFile>(file);
            settings = data;

            ConsoleMessage.Print("Settings applied with success.", "success");
        }

        private void SetWebDriver(string webDriverName, string webDriverPath)
        {
            ConsoleMessage.Print("Setting webdriver config.");

            string name = webDriverName.ToLower();

            switch (name)
            {
                case "edge":
                    var options = new EdgeOptions();
                    options.UseChromium = true;
                    options.PageLoadStrategy = PageLoadStrategy.Eager;
                    ConsoleMessage.Print($"Open {name} webdriver...", "warning");
                    webDriver = new EdgeDriver(webDriverPath, options);
                    break;
                default:
                    throw new Exception("WebDriver is invalid.");
            }

            if (bool.Parse(settings.fullscreen))
            {
                webDriver.Manage().Window.Maximize();
            }

            ConsoleMessage.Print("Webdriver configuration finished with success.", "success");
        }

        private IWebElement FindWebElementBy(string name, string target)
        {
            try
            {
                IWebElement element;

                switch (name)
                {
                    case "id":
                        element = FindWebElementById(target);
                        break;
                    case "name":
                        element = FindWebElementByName(target);
                        break;
                    case "xPath":
                        element = FindWebElementByXPath(target);
                        break;
                    case "js":
                        element = FindWebElementByJS(target);
                        break;
                    default:
                        element = null;
                        break;
                }

                return element;
            }
            catch(Exception error)
            {
                ConsoleMessage.Print($"{error.Message}", "error");
                return null;
            }
        }

        private IWebElement FindWebElementById(string target)
        {
            try
            {
                return wait.Until(e => e.FindElement(By.Id(target)));
            }
            catch(NoSuchElementException)
            {
                return null;
            }
        }

        private IWebElement FindWebElementByName(string target)
        {
            try
            {
                return wait.Until(e => e.FindElement(By.Name(target)));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        private IWebElement FindWebElementByXPath(string target)
        {
            try
            {
                return wait.Until(e => e.FindElement(By.XPath(target)));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        private IWebElement FindWebElementByJS(string script)
        {
            try
            {
                if (!script.StartsWith("return"))
                    ConsoleMessage.Print($"You script doesn't have 'return' keyword. Validate you script and add a 'return' keyword.\n\rCode: {script}", "warning");

                IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                return (IWebElement) wait.Until(e => ((IJavaScriptExecutor)e).ExecuteScript(script));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        private void ExecuteJavaScript(string script)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                wait.Until(e => ((IJavaScriptExecutor)e).ExecuteScript(script));
            }
            catch(Exception error)
            {
                Console.WriteLine($"{error.Message}");
            }
        }

        private void MakeAnAction(List<Entities.Action> actions)
        {
            foreach(var action in actions)
            {
                IWebElement element;

                switch (action.type)
                {
                    case "click":
                        ConsoleMessage.Print($"Trying click on element with {action.by}: '{action.data.element}'");
                        element = FindWebElementBy(action.by, action.data.element);
                        if (element == null)
                            throw new NoSuchElementException($"Element '{action.data.element}' doesn't exists.");
                        element.Click();
                        ConsoleMessage.Print($"Element has clicked with success.", "success");
                        break;
                    case "find":
                        ConsoleMessage.Print($"Trying find element with {action.by}: '{action.data.element}'");
                        element = FindWebElementBy(action.by, action.data.element);
                        if (element == null)
                            throw new NoSuchElementException($"Element '{action.data.element}' doesn't exists.");
                        ConsoleMessage.Print($"Element has found with success.", "success");
                        break;
                    case "fill":
                        ConsoleMessage.Print($"Trying fill element with {action.by}: '{action.data.element}'");
                        element = FindWebElementBy(action.by, action.data.element);
                        if (element == null)
                            throw new NoSuchElementException($"Element '{action.data.element}' doesn't exists.");
                        element.SendKeys(action.data.text);
                        ConsoleMessage.Print($"Element filled successfully.", "success");
                        break;
                    default:
                        break;
                }
            }
        }

        private void ReadWebPages(List<Page> pages)
        {
            foreach(var page in pages)
            {
                webDriver.Navigate().GoToUrl(page.url);
                ExecuteJavaScript(@"function check() { let res = setInterval(function() { if(document.readyState === 'complete') { return true; } }, 1000); return res;} return check();");
                MakeAnAction(page.actions);
            }
        }

        public void Open()
        {
            try
            {
                ReadWebPages(settings.websites);
            }
            catch(NoSuchElementException error)
            {
                ConsoleMessage.Print(error.Message, "error");
            }
            catch(TimeoutException error)
            {
                ConsoleMessage.Print(error.Message, "error");
            }
            catch(Exception error)
            {
                ConsoleMessage.Print(error.Message, "error");
            }
            finally
            {
                ConsoleMessage.Print($"Closing webdriver...", "warning");
                webDriver.Quit();
            }
        }
    }
}