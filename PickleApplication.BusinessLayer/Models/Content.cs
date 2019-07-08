namespace PickleApplication.BusinessLayer.Models
{
    public class Content
    {
        public int ContentId { get; set; }
        public string ContentTitle { get; set; }
        public string ContentUrl { get; set; }
        public bool ContentIsActive { get; set; }
        public string ContentEditorText { get; set; }
        public Link Link { get; set; }
    }
}
