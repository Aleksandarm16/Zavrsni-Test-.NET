using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AleksandarMihaljev.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        [Required]
        [StringLength(140)]
        public string NameAndLastName { get; set; }
        [Required]
        [Range(1899, 2021)]
        public int Year { get; set; }
        [Required]
        [StringLength(200)]
        public string Adress { get; set; }
        [Required]
        [StringLength(20)]
        public string CardType { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
    }
}