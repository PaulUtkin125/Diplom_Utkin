﻿using Finansu.Model;

namespace Diplom_Utkin.Model.DataBase
{
    public class BalanceHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public User User { get; set; }
        public int UserId { get; set; }
        public double Money { get; set; } = 0;
    }
}
