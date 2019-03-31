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
    public class EFDescriptionRepository : IDescriptionRepository
    {
        // контекст данных
        EFDbContext context = new EFDbContext();
        //путь по умолчанию
        const string path = "~/Files/User/";
        //данные с таблицы DescriptionUser
        public IEnumerable<DescriptionUser> DescriptionUsers
        {
            get { return context.DescriptionUsers; }
        }
        // сохранение сообщений
        public void SaveDescription(DescriptionUser userDescription)
        {
            context.DescriptionUsers.Add(userDescription);
            context.SaveChanges();
        }
        //прикрепить файл
        public string AddFileDescription(int ID, HttpPostedFileBase upload)
        {
            DirUser dirUser = new DirUser();
            string nameFile = "";

            string catalog = ID.ToString();
            string dir = "";
            DirectoryInfo directory = dirUser.CreateDir(path, catalog, "upload");
            dir = directory.ToString();
            try
            {
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

                            //dirUser.DeleteFile(directory);
                            upload1.SaveAs(dir + fileName);

                            nameFile = fileName;
                        }
                    }
                }
            }
            catch (Exception) { }

            return nameFile;
        }
        //сообщения определеного пользователя
        public List<DescriptionUser> GetDescriptionUsers(int Id)
        {
            List<DescriptionUser> description = context.DescriptionUsers.Where(a => a.IdUser == Id).ToList();
            description.Reverse();
            return description;
        }
        //кщличество сообщений определеного пользователя
        public int GetCountDescriptionUser(int Id)
        {
          var desc  =context.DescriptionUsers.Where(a=>a.IdUser==Id).ToList();
            int countDescription = desc.Count;
            return countDescription;
        }
        //общее количество сообщений
        public string GetCountDescription()
        {
            var descripts = context.DescriptionUsers.Count();
            if (descripts > 0)
            {
                return descripts.ToString();
            }
            else
            {
                return null;
            }
        }
        //удаление определенного сообщения
        public void DeleteDescription(int Id)
        {
            var description = context.DescriptionUsers.Where(a => a.IdUser == Id).ToList();
            foreach(var des in description)
            {
                if(des.IdUser==Id)
                {
                    context.DescriptionUsers.Remove(des);
                    
                }
            }
            context.SaveChanges();
        }
        
    }
}
