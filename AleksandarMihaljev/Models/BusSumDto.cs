using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AleksandarMihaljev.Models
{
    public class BusSumDto
    {
        public int Id { get; set; }
        public string BusLine { get; set; }
        public int Year { get; set; }
        public int SumPassenger { get; set; }

    }
}