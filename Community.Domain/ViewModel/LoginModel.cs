using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.ViewModel
{
   public class LoginModel
    {
        //[Required]
        public string Email { get; set; }
        //[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
