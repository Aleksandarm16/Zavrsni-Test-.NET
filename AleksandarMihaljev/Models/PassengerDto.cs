using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AleksandarMihaljev.Models
{
    public class PassengerDto
    {
        public int Id { get; set; }
       
        public string NameAndLastName { get; set; }
        public int Year { get; set; }
        public string Adress { get; set; }
        public string CardType { get; set; }
        public string BusLine { get; set; }
    }
}