using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
   public class Post
    {
        public int Id { set; get; }
        public int IdUserPublicPost { set; get; }
        public int IdReadPost { set; get; }
        public int IdDescription { set; get; }
        public bool UserRead { set; get; }
        public string Status { set; get; }
    }
}
