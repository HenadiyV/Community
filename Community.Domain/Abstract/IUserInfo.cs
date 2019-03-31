using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Abstract
{
  public  interface IUserInfo
    {
        UserInfo GetUserInfo(int id);
        
    }
}
