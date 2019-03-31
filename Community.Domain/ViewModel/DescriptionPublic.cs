using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.ViewModel
{
 public   class DescriptionPublic
    {
        public DescriptionUser DescriptionUser { set; get; }
        public User Users { set; get; }
        public Post Posts { set; get; }
    }
}
