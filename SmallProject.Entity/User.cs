using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Entity
{
    public class User : EntityBase
    {
        [Required(ErrorMessage = "Adı alanı boş geçilemez."), DisplayName("Kullanıcı Adi")]
        [StringLength(15, ErrorMessage = "En fazla {1} karakter uzunluğunda olmalıdır.", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez."), DataType(DataType.Password),
        DisplayName("Şifre")]
        [StringLength(100, ErrorMessage = "En fazla {1} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Adı alanı boş geçilemez."), DisplayName("Adı")]
        [StringLength(50, ErrorMessage = "En fazla {1} karakter uzunluğunda olmalıdır.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyadı alanı boş geçilemez."), DisplayName("Soyadı")]
        [StringLength(50, ErrorMessage = "En fazla {1} karakter uzunluğunda olmalıdır.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-Posta alanı boş geçilemez."), DataType(DataType.EmailAddress),
        DisplayName("E-Posta Adresi")]
        [StringLength(100, ErrorMessage = "En fazla {1} karakter uzunluğunda olmalıdır.", MinimumLength = 5)]
        public string EMail { get; set; }
        [StringLength(200), ScaffoldColumn(false)]
        public string PassResetCode { get; set; }

        // 64x64 thumbnail avatar image
        [ScaffoldColumn(false), DisplayName("Avatar")]
        public byte[] Avatar { get; set; }

        // orginal avatar image
        [ScaffoldColumn(false), DisplayName("Avatar")]
        [StringLength(100)]
        public string AvatarFull { get; set; }

     

    }
}
