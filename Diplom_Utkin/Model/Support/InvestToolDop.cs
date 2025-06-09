using System.ComponentModel.DataAnnotations;
using Finansu.Model;

namespace Diplom_Utkin.Model.Support
{
    public class InvestToolDop
    {
        public Portfolio portfolio { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public double Pribl { get; set; }
    }
}
