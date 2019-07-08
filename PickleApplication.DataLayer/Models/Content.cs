using PickleApplication.DataLayer.Core;
using System;

namespace PickleApplication.DataLayer.Models
{
    public class Content : Entity<int>
    {
        public Content()
        {
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
        public int LinkId { get; set; }
        public Link Link { get; set; }
        public string ContentTitle  { get; set; }
        public string ContentUrl { get; set; }
        public bool ContentIsActive { get; set; }
        public string ContentEditorText { get; set; }
    }
}
