using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaska.Data.Models
{
    public class ServiceBook
    {
        [Key]
        public int ServiceBookId { get; set; }

        [ForeignKey("ServiceBookLogId")]
        public int ServiceBookLogId { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public Car Car { get; set; }
        public ServiceBookLog ServiceBookLog { get; set; }
    }
}
