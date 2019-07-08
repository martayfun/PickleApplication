using System.ComponentModel.DataAnnotations;

namespace PickleApplication.BusinessLayer.Models
{
    public class Customer
    {
        public int CutomerId { get; set; }
        [Required(ErrorMessage ="Adınızı Giriniz!")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage ="Soyadınızı Giriniz!")]
        public string CustomerSurname { get; set; }
        [Required(ErrorMessage ="Posta Kodunu Giriniz!")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Cep Numarasını Giriniz!")]
        public string MobilePhone { get; set; }
        [Required(ErrorMessage = "İş Numarası Giriniz!")]
        public string CompanyPhone { get; set; }
        [Required(ErrorMessage = "E-Postanızı Giriniz!")]
        public string Mail { get; set; }
    }
}
