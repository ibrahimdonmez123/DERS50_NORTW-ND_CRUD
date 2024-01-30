using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DERS50_NORTWİND_CRUD.Models
{
    public class Product
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int ProductID { get; set; }

        [Required(ErrorMessage ="Ürün adı girmek zorunludur.")]
        [DisplayName("ÜRÜN ADI")] //.cshtml sayfasında formda labelda görünecek kısım.
        [MaxLength(40)] //nvarchar(40) ın karşılığı.
        [MinLength(3)] //min karakter uzunluğu

        public string? ProductName { get; set; }


        [Required(ErrorMessage ="Fiyat girmek zorunludur.")]
        [DisplayName("FİYAT ADI")]

        //encapsulation=kapsülleme
        // NEGATİF(-) DEĞER KAYDETMESİN SADECE POZİTİF KAYDETSŞN DİYE YAPTIK.
        private decimal _UnitPrice { get; set; }
        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = Math.Abs(value); }
        }

    }
   
}
