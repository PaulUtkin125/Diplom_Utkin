using System.ComponentModel.DataAnnotations;

namespace Finansu.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8 симвалов")]
        public string Loggin { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8 симвалов")]
        public string PaswordHash { get; set; }


        public TypeOfUser TypeOfUser { get; set; }
        public int TypeOfUserId { get; set; } = 2;

        public double Maney { get; set; } = 0;
    }
}
