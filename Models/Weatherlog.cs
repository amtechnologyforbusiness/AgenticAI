namespace Agentic.Models
{
    public class Weatherlog
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Temperature { get; set; }
        public string Condition { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}
