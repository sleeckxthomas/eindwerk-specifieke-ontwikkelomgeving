using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace project
{
    public class gebruiker
    {
        [Key]
        public int gebruiker_id { get; set; }
        public string naam { get; set; }
        public string paswoord { get; set; }
    }
}
