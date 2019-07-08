namespace PickleApplication.BusinessLayer.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public City City { get; set; }
    }
}
