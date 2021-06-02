using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace project
{
    public class registratie
    {
        [Key]
        public int registratie_id { get; set; }
        public string seizoen { get; set; }
        public DateTime vangst_tijd { get; set; }
        public float water_temperatuur { get; set; }
        public int luchtdruk { get; set; }
        public string windrichting { get; set; }
        public float diepte_rig { get; set; }
        public string baiting { get; set; }

    }
}
