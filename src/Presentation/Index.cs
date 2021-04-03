namespace Presentation
{
    public class Index
    {
        public static void Main()
        {
                Automation automation = new Automation(
                    @"D:\Repositories\automate-website-actions\src\Presentation\config\webdriver-settings.json",
                    @"D:\Repositories\automate-website-actions\src\Presentation\webdrivers\"
                );
                automation.Start();
        }
    }
}
