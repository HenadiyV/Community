using Community.Domain.Abstract;
using Community.Domain.Entities;
using Community.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Community.Domain.Concrete
{
  public  class EFPostRepository:IPostRepository
    {
        //контекст данных
        EFDbContext context = new EFDbContext();
        //путь по умолчанию
       const string path = "~/Files/User/";
        //данные таблицы Post
        public IEnumerable<Post> Posts
        {
            get { return context.Posts; }
        }
      // сохранить коментарий
        public void SavePost(int IdCreate, int IdRead,string name_file,string status)
        {
            DirUser dirUser = new DirUser();
            string create =IdCreate.ToString();
            string dirFrend = IdRead.ToString();
            string _status = GetPostStatus(status);
            // string download= path + dirFrend + "/upload/";
            DirectoryInfo directory = dirUser.CreateDir(path, create,"upload");
            string dir = directory.ToString();
            dir = dir + name_file;
            DirectoryInfo directoryFrend = dirUser.CreateDir(path, dirFrend,"upload");
            string dirF = directoryFrend.ToString();
            if (name_file != "")
            {
                MyMoveFile(dir, dirF);
            }
            int IdDescriptions = context.DescriptionUsers.Max(a => a.IdDescription);
            Post post = new Post { IdReadPost = IdRead, IdUserPublicPost = IdCreate, IdDescription=IdDescriptions, UserRead=false, Status=_status };
            context.Posts.Add(post);
            context.SaveChanges();
        }
        // масовая рассылка
        public void SavePostList(int Id,List<int> Id_user, string name_file, string status)
        {
            DirUser dirUser = new DirUser();
            string create = Id.ToString();
            string _status = GetPostStatus(status);
            DirectoryInfo directory = dirUser.CreateDir(path, create,"upload");
            string dir = directory.ToString();
            dir = dir + name_file;
            int IdDescriptions = context.DescriptionUsers.Max(a => a.IdDescription);
            foreach (int us in Id_user)
            {
            //    string dirFrend1 = us.ToString();
            //    DirectoryInfo directoryFrend = dirUser.CreateDir(path, dirFrend1,"upload");
            //string dirF = directory.ToString();
            //if (name_file != "")
            //{
            //    MyMoveFile(dir,dirF );
            //}
                
              
                Post post = new Post();
                post.IdUserPublicPost = Id;
                post.IdReadPost = us;
                post.IdDescription = IdDescriptions;
                post.UserRead = false;
                post.Status = _status;
                  context.Posts.Add(post);
            }
          
            context.SaveChanges();
        }
        // отправка друзьям
        public List<Coments> PostUser(int Id)
    {
        List<Coments> usComent = new List<Coments>();
            List<User> frend = new List<User>();
           
          
        var user = context.Users.Where(a => a.Id == Id).FirstOrDefault();
            var postFrend= context.Posts.Where(a => a.IdReadPost == Id).ToList();
            var desk = context.DescriptionUsers.ToList();
            foreach(var fr in postFrend)
            {
                User tempUs = context.Users.Where(a => a.Id == fr.IdUserPublicPost).FirstOrDefault();
                frend.Add(tempUs);
               
            }
            foreach(var pFr in postFrend)
            {
                        User us = context.Users.Where(a => a.Id == pFr.IdUserPublicPost).FirstOrDefault();
              
                        DescriptionUser de=context.DescriptionUsers.Where(des => des.IdDescription == pFr.IdDescription).FirstOrDefault();
                Post post = context.Posts.Where(s => s.IdReadPost == pFr.IdReadPost&&s.IdDescription== pFr.IdDescription).FirstOrDefault();
                            Coments coment = new Coments();
                            coment.IdUser = us.Id;
                            coment.NameUser = us.Name;
                            coment.NameFile = de.NameFile;
                        coment.IdDescription = de.IdDescription;
                        coment.Description = de.Description;
                
                            coment.Date = de.Date;
                coment.ReadUser = post.UserRead;
             
                            usComent.Add(coment);
            }
             usComent.Reverse();
            return usComent;
    }
        //перемещение файла
        public void MyMoveFile(string upload , string download)
        {
            string file = Path.GetFileName(upload);
            string newPath = Path.Combine(download, file);
            File.Move(upload, newPath);
        }
        //кому отправляем
        public string GetPostStatus(string status)
        {
            string _status = "";
            switch(status){
                case "1": _status = "Всем"; break;
                case "2": _status = "Друзьям"; break;
                case "3": _status = "Приватное"; break;
                case "4": _status = "Админ"; break;
            }
            return _status;
        }
      //от админа
        public void DefaultPost(int Id_ReadUser)
        {
            if (Id_ReadUser > 0) { 
            Post post = new Post() { IdDescription = 1, IdReadPost = Id_ReadUser, IdUserPublicPost = 1, Status = "Приватное" };
                context.Posts.Add(post);
                context.SaveChanges();
                }
        }
        //список получателей
        public List<User> GetListUserReadDescription(int Id, int IdDescription)
        {
            List<User> ListUserRead = new List<User>();
            var userPost = context.Posts.Where(s => s.IdUserPublicPost == Id).ToList();
            foreach(var user in userPost )
            {
                if(user.IdDescription==IdDescription)
                {
                    User us = context.Users.Where(s => s.Id == user.IdReadPost).FirstOrDefault();
                    ListUserRead.Add(us);
                }
            }
            ListUserRead.Reverse();
            return ListUserRead;
        }
        //отметить ка прочитанное
        public void UserReadPost(string IdUser,string IdDescript)
        {
            int IdUs = 0;
            int IdDesc = 0;
            int.TryParse(IdUser, out IdUs);
            int.TryParse(IdDescript, out IdDesc);
            if (IdUs > 0 && IdDesc > 0) {
                var post = context.Posts.Where(s => s.IdReadPost == IdUs && s.IdDescription == IdDesc).FirstOrDefault();
                if (post != null)
                {
                    post.UserRead = true;
                    context.Entry(post).State = EntityState.Modified;
                    context.SaveChanges();
                }
                    }
        }
        //количество непрочитанных сообщений
        public int NotReadPost(int Id)
        {
            int result = 0;
            var user = context.Posts.Where(s => s.IdReadPost == Id && s.UserRead == false).ToList();
            if (user != null) {
                result= user.Count;
                
            }
            return result;
        }
        //удаление
        public void DeleteUserPost(int Id)
        {
            var userPost = context.Posts.ToList();
            if(userPost.Count>0)
            {
                foreach(var us in userPost)
                {
                    if(us.IdUserPublicPost==Id||us.IdReadPost==Id)
                    {
                        context.Posts.Remove(us);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}

