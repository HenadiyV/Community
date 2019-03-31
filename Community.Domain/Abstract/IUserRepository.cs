using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Abstract
{
  public  interface IUserRepository
    {
       IEnumerable<User> Users{get;}
        bool SaveUser(User user);
        void UserDelete(int Id);
        void AddUser(User user);
       User GetUser(int Id);
        List<User> GetSearchUser(string NameUser);
        int GetIdNewUser();

        List<User> GetSearchUserName(string name);
        List<User> GetSearchUserFirstName(string firstname);
        List<User> GetSearchUserSurName(string surname);
        List<User> GetSearchUserGender(string gendername);
       // List<User> GetUserDate(string start, string stop);
        List<User> GetUserDate(string start);
        string GetUserFIO(int Id);
        string GetCountUsers();
        bool UserEmail(string email);
        int GetUroven(int Id);
    }
}
