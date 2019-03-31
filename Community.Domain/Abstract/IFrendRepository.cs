using Community.Domain.Entities;
using Community.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Abstract
{
  public  interface IFrendRepository
    {
        IEnumerable<Frends> Frends {  get; }
        List<User> GetFrends(int Id);
       List<UserSeting> GetFrendsSeting(int Id);
        bool AddFrend(int Id_user, int Id_frend);
            bool FrendRemove(int Id_user, int Id_frend);
        int GetCountFrend(int Id);
        string PersonaFrend(int Id_user, int Id_frend);
        List<User> GetBithdeyFrend(int Id);
        int GetCountFrendBithDay(int Id);
        List<MyFrend> GetCountDay(int Id);
        void DeleteUserFrend(int Id);
    }
}
