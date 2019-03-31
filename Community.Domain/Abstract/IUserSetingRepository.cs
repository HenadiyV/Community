using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Community.Domain.Abstract
{
   public interface IUserSetingRepository
    {
        IEnumerable<UserSeting> UserSetings {  get; }
       void SaveSeting(UserSeting user_seting);
       void UserAvatar(int ID, HttpPostedFileBase upload);
       void SetingUserDelete(int Id);
        void AddUserSeting(UserSeting userSeting);
        UserSeting GetUserSeting(int Id);
        void DefaultUserSeting(int Id);
        // bool SaveAvatar(int Id, string nameFile);
        List<UserSeting> GetUserSearchUserSeting(List<User> user);
        string GetAvatar(int Id);
    }
}
