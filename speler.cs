using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace eindwerk_ontwikkelingomgeving
{
    public class speler
    {
        [Key]
        public int spelers_id { get; set; }
        public string naam { get; set; }
        public int score { get; set; }
    }
}
