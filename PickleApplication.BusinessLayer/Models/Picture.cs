namespace PickleApplication.BusinessLayer.Models
{
    public class Picture
    {
        public string PictureId { get; set; }
        public int ProductId { get; set; }
        public string Extension { get; set; }
        public bool IsActive { get; set; }
    }
}
