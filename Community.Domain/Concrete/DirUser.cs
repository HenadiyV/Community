using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Community.Domain.Concrete
{
   public class DirUser
    {
        //путь к папке куда будем помещать пользовательские папки,файлы 
        const string DefaultPath = "~/Files/User/";
        // проверка расширения файла
        public bool ControlFile(string nameFile)
        {
            if (nameFile == ".png" || nameFile == ".jpg")
            {
                return true;

            }
            if (nameFile == ".jpeg")
            {
                return true;
            }
            return false;
        }
        //удаление файла
        public void DeleteFile(DirectoryInfo directory)
        {
            foreach (FileInfo fileOld in directory.GetFiles())
            {
                fileOld.Delete();
            }
        }
        // создание директории
        public DirectoryInfo CreateDir(string path, string catalog, string name)
        {
            try
            {
                if (name != "") name = name + "/";
                DirectoryInfo directory = new DirectoryInfo(HttpContext.Current.Request.MapPath(path + "/" + catalog + "/"+name ));
                if (!directory.Exists)
                {
                    Directory.CreateDirectory(directory.ToString());
                }
                return directory;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //сщздание директории под аватар
        public void CreateNewDirectory(int Id)
        {
            string catalog = Id.ToString();
            string dir = "";
            DirectoryInfo directory = new DirectoryInfo(HttpContext.Current.Request.MapPath(DefaultPath + "/" + catalog + "/avatar/" ));
            if (!directory.Exists)
            {
                Directory.CreateDirectory(directory.ToString());
            }
            dir = directory.ToString();
            
            DirectoryInfo directory1 = new DirectoryInfo(HttpContext.Current.Request.MapPath(DefaultPath + "/" + catalog + "/upload"));
            if (!directory1.Exists)
            {
                Directory.CreateDirectory(directory1.ToString());
            }
            CopyAvatar(dir);
        }
        // копирование аватарки в созданую директорию
        public void CopyAvatar(string dir)
        {
            string _path_image = System.IO.Path.GetFullPath(HttpContext.Current.Request.MapPath(@"~/Files/User/no-photo.png"));
            string temp = dir + Path.GetFileName(_path_image);
            FileInfo f = new FileInfo(_path_image);
            f.CopyTo(temp, true);
        }
    }
}
