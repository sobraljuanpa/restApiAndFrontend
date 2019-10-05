﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
