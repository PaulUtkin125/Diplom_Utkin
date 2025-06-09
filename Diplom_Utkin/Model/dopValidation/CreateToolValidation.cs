using System.ComponentModel.DataAnnotations;

namespace Diplom_Utkin.Model.dopValidation
{
    public class CreateToolValidation
    {
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public IFormFile file { get; set; }
        public string BrokersId { get; set; }

        //[RegularExpression("^[^0-9]*$", ErrorMessage = "Недопустимый символ: цифра")]
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [MinLength(5, ErrorMessage = "Минимальная длина: 5 симвалов")]
        public string NameInvestTool { get; set; }
        
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public string Price { get; set; }
        public string TypeTool { get; set; }
    }
}
