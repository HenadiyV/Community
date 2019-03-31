using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
    //не используеться в проекте
  public  class LogUser
    {
        [Key]
        public int IdLog { set; get; }
        public int IdUser { set; get; }
        public int IdDescription { set; get; }
        public DateTime DateLog { set; get; }
        public string NameFile { set; get; }
        public string Info { set; get; }
    }
}
