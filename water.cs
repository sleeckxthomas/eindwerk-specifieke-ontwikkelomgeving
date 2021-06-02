using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace project
{
    public class water
    {
        [Key]
        public int water_id { get; set; }
        public int gemeente_id { get; set; }
        public string naam { get; set; }
    }
}
