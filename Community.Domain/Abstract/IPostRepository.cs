using Community.Domain.Entities;
using Community.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Abstract
{
  public  interface IPostRepository
    {
     IEnumerable<Post> Posts { get; }
       
        void SavePost(int IdCreate, int IdRead, string nameFile,string status);
        void SavePostList(int Id,List<int> Id_user,string nameFile, string status);
        List<Coments> PostUser(int Id);
        void DefaultPost(int Id_ReadUser);
        List<User> GetListUserReadDescription(int Id, int IdDescription);
        void UserReadPost(string IdUser, string IdDescript);
        int NotReadPost(int Id);
        void DeleteUserPost(int Id);
    }
}
