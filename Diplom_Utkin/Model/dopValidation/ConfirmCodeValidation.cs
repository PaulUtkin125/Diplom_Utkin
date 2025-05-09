using System.ComponentModel.DataAnnotations;

namespace Diplom_Utkin.Model.dopValidation
{
    public class ConfirmCodeValidation
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [StringLength(3, ErrorMessage = "Код имеет длину в три символа!")]
        [MaxLength(3, ErrorMessage = "Код имеет длину в три символа!")]
        [MinLength(3, ErrorMessage = "Код имеет длину в три символа!")]
        public string securityCode {  get; set; }
    }
}
