using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.ViewModel
{
   public class SearchUserResult
    {
        public User User { set; get; }
        public UserSeting UserSeting { set; get; }
        public List<SearchUserResult> GetListSearchUserResult(List<User> user, List<UserSeting> userSeting)
        {
            List<SearchUserResult> resultSearch = new List<SearchUserResult>();
            if (user != null && userSeting != null)
            {
                foreach (var us in user)
                {
                    SearchUserResult newSearch = new SearchUserResult();
                    newSearch.User = us;
                    newSearch.UserSeting = userSeting.Where(a => a.IdUser == us.Id).FirstOrDefault();
                    if (newSearch != null)
                    {
                        resultSearch.Add(newSearch);
                    }
                }
                return resultSearch;
            }
            else return null;
        }
    }
}
