using System.ComponentModel.DataAnnotations;

namespace Diplom_Utkin.Model.Support
{
    public class CreateBrokerRequest
    {
        public IFormFile? file { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Недопустимый символ: цифра")]
        public string NameBroker { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Недопустимый символ: цифра")]
        public string FullNameOfTheDirector { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public string INN { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public string KPP { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public string OKTMO { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public string Phone { get; set; }
        public string BusinessAddress { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
        public string Email { get; set; }
        public int UrisidikciiyId { get; set; }
    }
}
