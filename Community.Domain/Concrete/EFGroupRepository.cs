using Community.Domain.Abstract;
using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//в проекте неиспользуеться
namespace Community.Domain.Concrete
{
   public class EFGroupRepository:IGroupRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Group> Groups
        {
            get { return context.Groups; }
        }
    }
}
