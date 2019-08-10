using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gaska.Data.Models
{
    public class ServiceBookLog
    {
        [Key]
        public int ServiceBookLogId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public long Mileage { get; set; }

        public string ServiceType { get; set; }

        [DataType(DataType.Date)]
        public DateTime NextServiceDate { get; set; }

        public long NextServiceMileage { get; set; }

        public string CarWork { get; set; }

        public string Details { get; set; }
    }
}
