using System.ComponentModel.DataAnnotations;

namespace Diplom_Utkin.Model.Support
{
    public class CardModel
    {
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [MaxLength(19)]
        [MinLength(19, ErrorMessage = "Номер карты указан не полностью!")]
        //[CreditCard]
        public string Number { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [MinLength(5, ErrorMessage = "Поле должно быть заполнено!")]
        [MaxLength(5)]
        public string ActivityDate { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [MinLength(3, ErrorMessage = "Поле должно быть заполнено!")]
        [MaxLength(3)]
        public string SVS_Code { get; set; }
    }
}
