using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace project
{
    public class vangst
    {
        [Key]
        public int vangst_id { get; set; }
        public int karper_id { get; set; }
        public int registratie_id { get; set; }
        public int water_id { get; set; }

    }
}
