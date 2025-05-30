using System.ComponentModel.DataAnnotations;
using Diplom_Utkin.Model.DataBase;

namespace Finansu.Model
{
    public class Brokers
    {
        public int Id { get; set; }
        public Urisidikciiy Urisidikciiy { get; set; }
        public int UrisidikciiyId { get; set; }
        public string NameBroker { get; set; }

        public string SourseFile { get; set; }
        public bool IsClosing { get; set; } = false;

        public string FullNameOfTheDirector { get; set; }

        public long INN {  get; set; }
        public long KPP { get; set; }
        public long OKTMO { get; set; }
        
        public string Phone { get; set; }
        public string BusinessAddress { get; set; }

        [Required(ErrorMessage = "Адрес электронной почты обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
        public string Email { get; set; }
        public bool isAdmitted { get; set; } = false;
        public DateOnly dateSubmitted { get; set; } = new DateOnly();
        public TypeOfRequest TypeOfRequest { get; set; }
        public int TypeOfRequestId { get; set; } = 1;
        public string? Password { get; set; } = null;
    }
}
