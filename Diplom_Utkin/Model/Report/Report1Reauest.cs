using System.ComponentModel.DataAnnotations;

namespace Diplom_Utkin.Model.Report
{
    public class Report1Reauest
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public DateTime startDate { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public DateTime endDate { get; set; }
        public int mode { get; set; }
    }
}
