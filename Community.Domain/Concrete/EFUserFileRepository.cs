using Community.Domain.Abstract;
using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Concrete
{
    // неиспользуеться в проекте
   public class EFUserFileRepository:IUserFileRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<UserFile> UserFiles
        {
            get { return null; }
        }
    }
}
