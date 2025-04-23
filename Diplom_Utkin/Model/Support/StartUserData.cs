using System.ComponentModel.DataAnnotations;

namespace DiplomAPI.Models.Support
{
    public class StartUserData
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8 симвалов")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8 симвалов")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])(?=.*[0-9]).+$" ,ErrorMessage = "Поле должно содержать: спецсимвол, строчную и заглавную латинскую букву, цифру")]
        public string Password { get; set; }
    }
}
