using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoDrive.Domain
{
    public class LogItem
    {

        public LogItem(long userId, DateTime? date, int count)
        {
            this.Date = date;
            this.UserId = userId;
            this.Count = count;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime? Date { get; set; }
        public int Count { get; set; }
    }
}
