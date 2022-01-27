using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AleksandarMihaljev.Models
{
    public class Bus
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Line { get; set; }
        [Required]
        [Range(2000,2021)]
        public int Year { get; set; }
    }
}