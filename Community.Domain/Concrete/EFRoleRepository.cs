using Community.Domain.Abstract;
using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Community.Domain.Concrete
{
    public class EFRoleRepository : IRoleRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Role> Roles
        {
            get { return context.Rolies; }
        }
        
        public List<SelectListItem> RoleList()
        {
            List<SelectListItem> rolyList = new List<SelectListItem>();
           
            var rol = context.Rolies.ToList();
            foreach(Role r in rol)
            {
                rolyList.Add(new SelectListItem
                {
                    Value = r.IdRole.ToString(),
                Text = r.NameRole
                });
               
            }
            
            return rolyList;
        }
    }
}
