using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
    //не используеться в проекте
  public  class Group
    {
        [Key]
        public int IdGroup { set; get; }
        public string NameGroup { set; get; }
    }
}
