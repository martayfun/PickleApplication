using System.ComponentModel.DataAnnotations;

namespace PickleApplication.BusinessLayer.Models
{
    public class DeliveryInfo
    {
        public int DeliveryInfoId { get; set; }
        [Required(ErrorMessage = "Bölge Seçiniz!")]
        public Region Region { get; set; }
        [Required(ErrorMessage = "Adres Giriniz!")]
        public string Address { get; set; }
    }
}
