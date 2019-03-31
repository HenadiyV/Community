using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
   public class Frends
    {
        [Key]
        public int Id { set; get; }
        public  int IdUser { set; get; }
        public int IdFrend { set; get; }
    }
}
