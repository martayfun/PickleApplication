namespace PickleApplication.BusinessLayer.Models
{
    public class Link
    {
        public int LinkId { get; set; }
        public MainType Type { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsAssigned { get; set; }
    }
}
