using System.Collections.Generic;

namespace Domain.Entities
{
    public class Page
    {
        public string url { get; set; }
        public List<Action> actions { get; set; }
    }
}