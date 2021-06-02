using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace project
{
    public class gemeente
    {
        [Key]
        public int gemeente_id { get; set; }
        public int postcode { get; set; }
        public string gemeente_naam { get; set; }
        public string land { get; set; }
    }
}
