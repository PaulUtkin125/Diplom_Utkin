using System.ComponentModel.DataAnnotations;

namespace Diplom_Utkin.Model.DataBase
{
    public class News
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string HeadLine { get; set; }
        public string Body { get; set; }
    }
}
