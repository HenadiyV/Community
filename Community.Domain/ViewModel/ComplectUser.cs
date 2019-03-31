using Community.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
   public class ComplectUser
    {
        public UserInfo UserInfo { set; get; }
        //public List<DescriptionUser> Description { set; get; }
        //public List<Frends> Frend { set; get; }
        //public List<Post> Post { set; get; }
        public List<Coments> Coments { set; get; }
        public int countFrend { set; get; }
        public int countDescription { set; get; }
        public int countFrendBirthDay { set; get; }
    }
}
