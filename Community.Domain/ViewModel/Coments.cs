using Community.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.ViewModel
{
   public class Coments
    {
       
        public int    IdUser { set; get; }
        public int IdDescription { set; get; }
        public string NameUser { set; get; }
        public bool ReadUser { set; get; }
        public string Description { set; get; }
        public string NameFile { set; get; }
        public string Date { set; get; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
