using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PickleApplication.BusinessLayer.Models
{
    public class Product 
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Ürün ismi boş bırakılamaz!")]
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "Kategori boş bırakılamaz!")]
        public virtual Link Link { get; set; }
        [Required(ErrorMessage = "Başlık boş bırakılamaz!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Açıklama boş bırakılamaz!")]
        public string Description { get; set; }
        public int Stock { get; set; }
        public int Star { get; set; }
        [Required(ErrorMessage = "Fiyat boş bırakılamaz!")]
        public decimal UnitPrice { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
}
