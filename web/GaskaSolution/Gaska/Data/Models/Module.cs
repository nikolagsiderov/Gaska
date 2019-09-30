using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gaska.Data.Models
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        public string Name { get; set; }
    }
}
