namespace PickleApplication.BusinessLayer.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public Country Country { get; set; }
    }
}
