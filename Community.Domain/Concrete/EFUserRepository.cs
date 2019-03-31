using Community.Domain.Abstract;
using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Community.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
        //сохранение
        public bool SaveUser(User user)
        {
            bool res = true;
            var us = context.Users.Where(usr=>usr.Email==user.Email);
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
              
            foreach (var _us in us)
            {
                if (_us.Id != user.Id)
                { res = false; }
            }
            if (res)
            {
                UserRegistration(user);
                return true;
            }

            return false;
            
        }
        //если заполненно фамилия, отчество,день рождения,телефон-разрешить создание сообщений
        private void UserRegistration(User user)
        {
            if(user.FirstName!=null&&user.Surname!=null&&user.BirthDay!=null&&user.Phone!=null)
            {
                UserSeting usSeting = context.UserSetings.Find(user.Id);
                if (usSeting != null)
                {
                    usSeting.Post = true;
                    context.Entry(usSeting).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
        //пользователь
        public User GetUser(int Id)
        {
            return context.Users.Find(Id);
        }
        //добавление
        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        //удаление
       public void UserDelete(int Id)
        {
            var user = context.Users.Where(us => us.Id == Id).FirstOrDefault();
            if(user!=null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
        // поиск по ФИО
        public List<User> GetSearchUser(string NameUser)
        {
            List<User> users = new List<User>();
            users = context.Users.Where(a => a.Name == NameUser || a.FirstName == NameUser || a.Surname == NameUser).ToList();
            if (users != null)
            {
                return users;
            }
            else
                return null;
        }
        // Id вновь добавленого пользователя
        public int GetIdNewUser()
        {
            int Id = context.Users.Max(a => a.Id);
            if (Id > 0)
                return Id;
            else
                return 0;
        }
        //поиск по имени
        public List<User> GetSearchUserName(string name)
        {
            List<User> user = new List<User>();
            user = context.Users.Where(a => a.Name==name).ToList();
            return user;
        }
        // по отчеству
        public List<User> GetSearchUserFirstName(string firstname)
        {
            List<User> user = new List<User>();
            user = context.Users.Where(a => a.FirstName==firstname).ToList();
            return user;
        }
        //по  фамилии
        public List<User> GetSearchUserSurName(string surname)
        {
            List<User> user = new List<User>();
            user = context.Users.Where(a => a.Surname==surname).ToList();
            return user;
        }
        //гендерный поиск 
        public List<User> GetSearchUserGender(string gendername)
        {
            int Id_gender = 0;
            if(gendername== "Мужской")
            { Id_gender = 1; }else 
                if(gendername == "Женский")
            { Id_gender = 2; }
            if (Id_gender > 0) { 
            List<User> user = new List<User>();
            user = context.Users.Where(a => a.GenderId==Id_gender).ToList();
            return user;
            }else
            {
                return null;
            }
        }
        //проверка на существование даного email в базе
        public bool UserEmail(string email)
        {
            if(email!=null)
            {
                var eml = context.Users.Where(s => s.Email == email).FirstOrDefault();
                if(eml==null)
                {
                    return true;
                }
            }
            return false;
        }
        // уровень
        public int GetUroven(int Id)
        {
            User user = context.Users.Where(a => a.Id == Id).FirstOrDefault();
           
            return user.Uroven;
        }
        // поиск по годам
        public List<User> GetUserDate(string start)
        {
            List<User> usDate = new List<User>();
            
            string star = start.Substring(1, 2);
            var usBirthdey = context.Users.ToList();
            foreach(var us in usBirthdey)
            {
                User usr = new User();
                if (us.BirthDay != "") { 
                string d = us.BirthDay.Substring(1,2);
                if(d==star)
                { usDate.Add(us); }
                }
            }

            return usDate;
        }
        //Ф.И.О. пользователя
        public string GetUserFIO (int Id)
        {
            string FIO = "";
            User user = GetUser( Id);
            if (user != null) {
                FIO = user.Name + " " + user.FirstName + " " + user.Surname;
                    }
            return FIO;
        }
        //количество пользователей
        public string GetCountUsers()
        {
            var us = context.Users.Count();
            if (us > 0) { 
            return us.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
