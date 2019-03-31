using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
   public class Address
    {
        [Key]
        public int IdAddress { set; get; }
        public string Contry { set; get; }
        public string City { set; get; }
        public string Street { set; get; }
        public string Hous { set; get; }
        public string Apartment { set; get; }
        public int? IdUser { set; get; }
        public User User { set; get; }
       
    }
}
