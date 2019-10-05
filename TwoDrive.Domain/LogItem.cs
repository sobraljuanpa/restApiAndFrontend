using System;
using System.Collections.Generic;
using System.Text;

namespace TwoDrive.Domain
{
    public class LogItem
    {

        public LogItem(long userId, DateTime date)
        {
            this.Date = date;
            this.UserId = userId;
        }

        public long UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
