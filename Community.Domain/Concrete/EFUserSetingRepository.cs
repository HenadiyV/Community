using Community.Domain.Abstract;
using Community.Domain.Entities;
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
   public class EFUserSetingRepository:IUserSetingRepository
    {
        //путь по умолчанию
        const string path = "~/Files/User/";
        //контекст данных
        EFDbContext context = new EFDbContext();
        // данные с таблицы UserSeting
        public IEnumerable<UserSeting> UserSetings
        {
            get { return context.UserSetings; }
        }
        //сохранение
        public void SaveSeting(UserSeting user_seting)
        {
           context.Entry(user_seting).State = EntityState.Modified;
            context.SaveChanges();
        }
        // загрузка аватарки
        public void UserAvatar(int ID, HttpPostedFileBase upload)
        {

            DirUser dirUser = new DirUser();
            string nameFile = "";
            bool ok = false;
            string catalog = ID.ToString();
            string dir = "";
            DirectoryInfo directory =dirUser.CreateDir(path, catalog,"avatar");
            dir = directory.ToString();
           
            try { 
            foreach (string file in HttpContext.Current.Request.Files)
            {
                var upload1 = HttpContext.Current.Request.Files[file];
                if (upload1 != null)
                {
                    string contr = "";
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload1.FileName);
                    contr = fileName.Substring(fileName.LastIndexOf("."));
                    if (dirUser.ControlFile(contr))
                    {
                            
                            dirUser.DeleteFile(directory);
                            upload1.SaveAs(dir +fileName);
                            var user = context.UserSetings.Find(ID);

                            user.Avatar = fileName;
                            SaveSeting(user);
                            ok = true;
                    nameFile = fileName;
                   }
                }
            }
            } catch (Exception) { }
           
            if (!ok)// загрузить изображение по умолчанию 
            {
                string _path_image = System.IO.Path.GetFullPath(HttpContext.Current.Request.MapPath(@"~/Files/User/no-photo.png"));
                string temp = dir + Path.GetFileName(_path_image);
                FileInfo f = new FileInfo(_path_image);
                f.CopyTo(temp, true);
                nameFile = "no-photo.png";
                var user = context.UserSetings.Find(ID);

                user.Avatar = "no-photo.png";
                SaveSeting(user);
            }
           
        }
        //настройки отображения - удаление
        public void SetingUserDelete(int Id)
        {
            var seting = context.UserSetings.Find(Id);
            if(seting!=null)
            {
                context.UserSetings.Remove(seting);
                context.SaveChanges();
            }
        }
        //настройки отображения - добавление
        public void AddUserSeting(UserSeting userSeting)
        {
            context.UserSetings.Add(userSeting);
            context.SaveChanges();
        }
        // настройки отображения  пользователя
        public UserSeting GetUserSeting(int Id)
        {
            UserSeting usSeting =context.UserSetings.Where(a=>a.IdUser==Id).FirstOrDefault();
            return usSeting;
        }
        //настройки отображения по умолчанию
        public void DefaultUserSeting(int Id)
        {
            UserSeting usSeting = new UserSeting();
            usSeting.Address = true;
            usSeting.Age = true;
            usSeting.Email = true;
            usSeting.GreateGroup = false;
            usSeting.Phone = true;
            usSeting.Post = false;
            usSeting.Surname = true;
            usSeting.IdUser = Id;
            usSeting.Avatar = "no-photo.png";
            context.UserSetings.Add(usSeting);
            context.SaveChanges();
        }
        //настройки отображения пользователей
        public  List<UserSeting> GetUserSearchUserSeting(List<User> user)
        {
            List<UserSeting> userSetings = new List<UserSeting>();
            if (user!=null)
            {
                foreach (var us in user)
                {
                    UserSeting usSet = new UserSeting();
                    usSet = context.UserSetings.Where(a => a.IdUser == us.Id).FirstOrDefault();
                    if (usSet != null)
                    {
                        userSetings.Add(usSet);
                    }
                }
                return userSetings;
            }
            else return null;
        }
        //файл аватарки определеного пользователя
        public string GetAvatar(int Id)
        {
            string NameFile = "";
            var user= context.UserSetings.Find(Id);
            if(user!=null)
            {
                NameFile = user.Avatar;
            }
            return NameFile;
        }
    }
}
