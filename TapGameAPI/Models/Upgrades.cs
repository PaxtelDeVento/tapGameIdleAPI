namespace Upgrades.API.Model
{
    public class UpgradesModel
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Cost { get; set; }
        public int Cost_increment { get; set; }
        public string? Modifier { get; set; }
        public double Diamonds_increment { get; set; }
        public string? Type { get; set; }
    }
    
}