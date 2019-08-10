using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaska.Data.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        public string Color { get; set; }

        public string ProductionYear { get; set; }

        public string RegistrationPlate { get; set; }

        public string Owner { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public int Mileage { get; set; }

        [ForeignKey("dbo.AspNetUsers")]
        public string UserId { get; set; }

        public ICollection<ServiceBook> ServiceBooks { get; set; }
    }
}
