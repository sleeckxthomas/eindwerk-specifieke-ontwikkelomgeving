using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace project
{
    public class karper
    {
        [Key]
        public int karper_id { get; set; }
        public string type_karper { get; set; }
        public string naam_karper { get; set; }
        public float gewicht { get; set; }
        public float lengte { get; set; }
    }
}
