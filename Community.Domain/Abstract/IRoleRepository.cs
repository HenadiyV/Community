using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Community.Domain.Abstract
{
  public  interface IRoleRepository
    {
        IEnumerable<Role> Roles { get; }
        List<SelectListItem> RoleList();


    }
}
