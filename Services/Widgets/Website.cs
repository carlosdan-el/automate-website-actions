using System.Collections.Generic;
using Services.Widgets.Base;

namespace Services.Widgets
{
    public class Website
    {
        public string Name;
        public string WebDriver;
        public string Authentication;
        public string Username;
        public string Password;
        public bool Headless;
        public bool Printscreen;
        public List<Page> Pages;
    }
}