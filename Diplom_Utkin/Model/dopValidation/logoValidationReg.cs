using System.ComponentModel.DataAnnotations;

namespace Diplom_Utkin.Model.dopValidation
{
    public class logoValidationReg
    {
        [Required(ErrorMessage = "Адрес электронной почты обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8 симвалов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8 симвалов")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])(?=.*[0-9]).+$", ErrorMessage = "Поле должно содержать: спецсимвол, строчную и заглавную латинскую букву, цифру")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8 симвалов")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])(?=.*[0-9]).+$", ErrorMessage = "Поле должно содержать: спецсимвол, строчную и заглавную латинскую букву, цифру")]
        public string ConfirmPassword { get; set; }
    }
}
