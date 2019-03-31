using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
  public class UserSeting
    {
        [Key]
        public int Id { set; get; }
        public int IdUser { set; get;}
        public bool Age { set; get; }
        public bool Surname { set; get; }
        public bool Email { set; get; }
        public bool Phone { set; get; }
        public bool Address{ set; get; }
        public string Avatar { set; get; }
        public bool AdminSeting { set; get; }
        public bool UploadFile { set; get; }
        public bool Post { set; get; }
        public bool GreateGroup { set; get; }
    }
}
