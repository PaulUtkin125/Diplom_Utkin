using System.ComponentModel.DataAnnotations;

namespace Diplom_Utkin.Model.Report
{
    public class ReportTransactionOperatio_Request
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public int uId { get; set; }
    }
}
