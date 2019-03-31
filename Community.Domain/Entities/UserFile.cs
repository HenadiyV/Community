using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
    // в проекте не используеться
   public class UserFile
    {
    
        public int IdUpload { set; get; }
        public int IdUser { set; get; }
        public int IdFrend { set; get; }
        public string NameFile { set; get; }
        public string Date { set; get; }
       
    }
}
