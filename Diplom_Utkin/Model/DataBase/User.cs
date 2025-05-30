using System.ComponentModel.DataAnnotations;

namespace Finansu.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Адрес электронной почты обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
        public string Loggin { get; set; }
        public string? Phone { get; set; }
        public string PaswordHash { get; set; }
        public TypeOfUser TypeOfUser { get; set; }
        public int TypeOfUserId { get; set; } = 2;

        public double Maney { get; set; } = 0;
    }
}
