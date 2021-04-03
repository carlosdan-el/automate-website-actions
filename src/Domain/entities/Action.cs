namespace Domain.Entities
{
    public class Action
    {
        public string type { get; set; }
        public string by { get; set; }
        public Data data { get; set; }
    }
}