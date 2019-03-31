using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Community.Domain.Entities
{
   public class UserInfo
    {
        [HiddenInput(DisplayValue = false)]
        public int _IdUser { set; get; }
        [Display(Name = "Название")]
        public string _Name { set; get; }
        public string _FirstName { set; get; }
        public string _Surname { set; get; }
        public string _Email { set; get; }
        public string _Phone { set; get; }
        public string _BirthDay { set; get; }
        public string _Age { set; get; }
        public bool _AgeSeting { set; get; }
        public bool _SurnameSeting { set; get; }
        public bool _EmailSeting { set; get; }
        public bool _PhoneSeting { set; get; }
        public bool _AddressSeting { set; get; }
        public string _Avatar { set; get; }


    }
}
