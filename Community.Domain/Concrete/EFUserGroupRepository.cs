using Community.Domain.Abstract;
using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Concrete
{
    //неиспользуеться в проекте
  public  class EFUserGroupRepository:IUserGroupRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<UserGroup> UserGroups
        {
            get { return context.UserGroups; }
        }
        public void GroupDelete(int Id)
        {
            var group = context.UserGroups.Where(gr => gr.IdUser == Id).ToList();
            if(group!=null)
            {
                foreach(var g in group)
                {
                    context.UserGroups.Remove(g);
                }
                context.SaveChanges();
            }
        }
    }

}
