using Community.Domain.Concrete;
using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.ViewModel
{
    public class MyFrend
    {
        //public List<User> User { set; get; }
        //public List<UserSeting> UserSeting { set; get; }
        public User User { set; get; }
        public UserSeting UserSeting { set; get; }
        public string countDayMessage { set; get; }
        //public List<MyFrend> myFrends(List<User> user, List<UserSeting> userSeting)
        //{
        //    List<MyFrend> mFrend = new List<MyFrend>();

        //    if (user != null && userSeting != null)
        //    {
        //        foreach (var us in user)
        //        {
        //            MyFrend newSearch = new MyFrend();
        //            newSearch.User = us;
        //            newSearch.UserSeting = userSeting.Where(a => a.IdUser == us.Id).FirstOrDefault();
        //            if (newSearch != null)
        //            {
        //                mFrend.Add(newSearch);
        //            }
        //        }

        //        if (mFrend.Count > 0)
        //        {
        //            return mFrend;
        //        }
                


        //    }
        //    return null;
        //}
    }
}
