using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gaska.Data.Models
{
    public class Data
    {
        [Key]
        public int DataId { get; set; }

        public string DataString { get; set; }

        [ForeignKey("ModuleId")]
        public int ModuleId { get; set; }
    }
}
