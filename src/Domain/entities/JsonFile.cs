using System.Collections.Generic;

namespace Domain.Entities
{
    public class JsonFile
    {
        public string webdriver { get; set; }
        public string headless { get; set; }
        public string authType { get; set; }
        public string fullscreen { get; set; }
        public List<Page> websites { get; set; }
    }
}