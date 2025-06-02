using System.ComponentModel.DataAnnotations;
using Diplom_Utkin.Model.DataBase;

namespace Finansu.Model
{
    public class Brokers
    {
        public int Id { get; set; }
        public Urisidikciiy Urisidikciiy { get; set; }
        public int UrisidikciiyId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Недопустимый символ: цифра")]
        public string NameBroker { get; set; }

        public string SourseFile { get; set; }
        public bool IsClosing { get; set; } = false;

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [RegularExpression("^[^0-9]*$", ErrorMessage = "Недопустимый символ: цифра")]
        public string FullNameOfTheDirector { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public long INN {  get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public long KPP { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public long OKTMO { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]

        public string BusinessAddress { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
        public string Email { get; set; }
        public bool isAdmitted { get; set; } = false;
        public DateOnly dateSubmitted { get; set; } = new DateOnly();
        public TypeOfRequest TypeOfRequest { get; set; }
        public int TypeOfRequestId { get; set; } = 1;
        public string? Password { get; set; } = null;
    }
}
