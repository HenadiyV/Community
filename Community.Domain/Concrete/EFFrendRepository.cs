using Community.Domain.Abstract;
using Community.Domain.Entities;
using Community.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Community.Domain.Concrete
{
  public  class EFFrendRepository:IFrendRepository
    {
        //контекст данных
        EFDbContext context = new EFDbContext();
        //путь к папке с файлами
        const string pathBirthday = "~/Files/Upload/";
        // путь по умолчанию
        const string pathUser = "~/Files/User";
        //данные с таблицы Frends
        public IEnumerable<Frends> Frends
        {
            get { return context.Frends; }
        }
        //список друзей
        public List<User> GetFrends(int Id)
        {
            List<User> listUser = new List<User>();
            var frend = context.Frends.Where(us => us.IdUser == Id).ToList();
            foreach(var fr in frend)
            {
                User user = new User();
                user = context.Users.Where(us=>us.Id==fr.IdFrend).FirstOrDefault();

                listUser.Add(user);
            }
            if (listUser.Count > 0)
            {
                return listUser;
            }
            else return null;
        }
        //список настроек отображения информации друзей
        public List<UserSeting> GetFrendsSeting(int Id)
        {
            List<UserSeting> listUserSeting = new List<UserSeting>();
            var frend = context.Frends.Where(us => us.IdUser == Id).ToList();
            foreach (var fr in frend)
            {
                UserSeting user = new UserSeting();
                user = context.UserSetings.Where(us => us.Id == fr.IdFrend).FirstOrDefault();

                listUserSeting.Add(user);
            }
            if (listUserSeting.Count > 0)
            {
                return listUserSeting;
            }
            else return null;
        }
        //добавление в список друзей
        public bool AddFrend(int Id_user,int Id_frend)
        {
            if (ControlFrend(Id_user, Id_frend))
            {
                try
                {
                    var userFrend = new List<Frends>()
            {
                new Frends(){ IdUser = Id_user, IdFrend = Id_frend },
                new Frends(){  IdUser = Id_frend, IdFrend =Id_user }
            };
                    userFrend.ForEach(s => context.Frends.Add(s));
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            else { return false; }
        }
        //удаление из списка друзей
        public bool FrendRemove(int Id_user, int Id_frend)
        {
            if (!ControlFrend(Id_user, Id_frend))
            {
                var user = context.Frends.ToList();

                foreach (var us in user)
                {
                    if (us.IdFrend == Id_frend && us.IdUser == Id_user || us.IdFrend == Id_user && us.IdUser == Id_frend)
                    {
                        context.Frends.Remove(us);
                    }

                }
                
                context.SaveChanges();
                return true;
            }else
            return false;
        }
        //находиться ли данный пользователь в списке друзей
        public  bool ControlFrend(int Id_user,int Id_frend)
        {
            var element = context.Frends.Where(a => a.IdFrend == Id_frend && a.IdUser == Id_user).ToList();
            if(element.Count>0)
                {
                return false;
            }else return true;
        }
        //количество друзей
        public int GetCountFrend(int Id)
        {
            int fr = 0;
            var countFr = context.Frends.Where(a => a.IdUser==Id).ToList();
            fr = countFr.Count;
            return fr;
        }
        // ФИО друга
        public string PersonaFrend(int Id_user,int Id_frend)
        {
            string FIO = "";
            
            List<User> frends = new List<User>();
            frends = GetFrends(Id_user);
            foreach(User frend in frends)
            {
                if (frend.Id == Id_frend)
                {
                    FIO= frend.Name+" "+frend.FirstName+" "+frend.Surname;

                }
            }
            return FIO;
        }
        //список пользователей у которых день рождения в наступившем месяце 
        //и автоматическое поздравление системы с  денём рождения 
        public List<User> GetBithdeyUser(int Id)
        {
            string date = DateTime.Now.Date.ToShortDateString();
            List<User> user = context.Users.ToList();
            if (user != null)
            {
                List<User> userBithdey = new List<User>();
                DataNew dat = new DataNew();
                DataNew _data = new DataNew();
                DataNew BirthDay = new DataNew();
                DataNew _BirthDay = new DataNew();
                _data = dat.UserNewData(date);
                foreach (User us in user)
                {
                    BirthDay = _BirthDay.UserBirthDay(us.BirthDay);
                    if (BirthDay != null) { 
                    if (_data.month == BirthDay.month && _data.day <= BirthDay.day)
                    {
                            userBithdey.Add(us);

                     }
                    if (BirthDay.day > 0) { 
                        if (_data.month == BirthDay.month && _data.day == BirthDay.day && us.Uroven > 0)
                        {
                                CongratulationsForSystem(us.Id);
                        }
                        else if (us.Uroven==0)
                        {
                               
                                us.Uroven = 500;
                                try
                                {
                                   context.Entry(us).State = EntityState.Modified;
                                context.SaveChanges();
                                    
                                }
                                catch(Exception ex) { }
                            }
                        }
                    }
                }
                return userBithdey;
            }
            return null;
        }
        //список друзей укоторых день рождения в наступившем месяце
        public List<User> GetBithdeyFrend(int Id)
        {
            List<User> user = GetBithdeyUser(Id);
            user.RemoveAll(x => x.Id == Id);
            List<User> frend = GetFrends(Id);
            if (user != null && frend != null) {
                List<User> result = frend.Where(n => user.Any(t => t.Id == n.Id)).ToList();
                if (result != null)
                {
                   return result;
                }
               
            }
           
            return null;
           
        }
        //количество имениников
        public int GetCountFrendBithDay(int Id)
        {
            int countFrend = 0;
           List<User> usFrend = GetBithdeyFrend(Id);
            if (usFrend != null) {
                usFrend.RemoveAll(x => x.Id == Id);
             countFrend = usFrend.Count();
            }

            return countFrend;
        }
        //список для отображения имениников
        public List<MyFrend> GetCountDay(int Id)
        {
            List<MyFrend> myFrends = new List<MyFrend>();

            int countDay = 0;
            string mess = "";
            string date = DateTime.Now.Date.ToShortDateString();
            List<User> frend = GetFrends(Id);
            if (frend != null) { 
            List<User> frendBithdey = new List<User>();
            DataNew dat = new DataNew();
            DataNew _data = new DataNew();
            DataNew BirthDay = new DataNew();
            DataNew _BirthDay = new DataNew();
            _data = dat.UserNewData(date);
            foreach (User us in frend)
            {
                BirthDay = _BirthDay.UserBirthDay(us.BirthDay);
                if (_data.month == BirthDay.month&&_data.day<=BirthDay.day)
                {
                     UserSeting userSeting = context.UserSetings.Where(s=>s.Id==us.Id).FirstOrDefault();
                    countDay = BirthDay.day - _data.day;
                     mess=CountDayMessage(countDay);
                    MyFrend _myFrends = new MyFrend
                    {
                        User = us,
                        UserSeting = userSeting,

                        countDayMessage = mess
                    };
                    myFrends.Add(_myFrends);

                }
               
            }
            return myFrends;
            }
            else
            {
                return null;
            }
        }
        //формирование информационого сообщения
        public string CountDayMessage(int countDay)
        {
            int temp = 0;
            string frendBirthDay = "";
            if (countDay > 21)
            {
                temp = countDay % 10;
            }
            else temp = countDay;
            switch (temp)
            {
                case 0: frendBirthDay = " Сегодня!!!"; break;
                case 1: frendBirthDay = " через "+ countDay.ToString() + "  день "; break;
                case 2: frendBirthDay = " через " + countDay.ToString() + "  дня "; break;
                case 3: frendBirthDay = " через " + countDay.ToString() + "  дня "; break;
                case 4: frendBirthDay = " через " + countDay.ToString() + "  дня "; break;
                default: frendBirthDay = " через " + countDay.ToString() + "  дней "; break;
            }
            return frendBirthDay;
        }
        //автоматическое поздравление
        public void CongratulationsForSystem(int Id)
        {
            string date = DateTime.Now.Date.ToShortDateString();
            string deskript = "От всей души хотим поздравит Вас с Днем Рождения!!! Пожелать долгих лет жизни. Крепкого здоровья. А также реализаци Ваших идей и желаний. С уважением администрация сайта.";
            DescriptionUser congratulations = new DescriptionUser() { Date = date, Description = deskript, IdUser = Id, NameFile = "birthday.jpg" };
            context.DescriptionUsers.Add(congratulations);
            context.SaveChanges();
            int MaxId = context.DescriptionUsers.Max(s => s.IdDescription);
            Post post = new Post() { IdDescription = MaxId, IdReadPost = Id, IdUserPublicPost = 1, Status = "Приватное", UserRead=false };
            context.Posts.Add(post);
            context.SaveChanges();
            User us = context.Users.Where(s => s.Id == Id).FirstOrDefault();
            if (us != null)
            {
               
                us.Uroven = 0;
                try
                {
                    context.Entry(us).State = EntityState.Modified;
                    context.SaveChanges();

                }
                catch (Exception ex) { }
               
            }

        }
        //копироание файлов
        public void MyMoveFile(int Id)
        {
            DirUser dirUser = new DirUser();
            string catalog = Id.ToString();
            DirectoryInfo directory = dirUser.CreateDir(pathUser, catalog, "upload");
            string dir = directory.ToString();
            string _path_image = System.IO.Path.GetFullPath(HttpContext.Current.Request.MapPath(@"~/Files/Upload/birthday.png"));
            string temp = dir + Path.GetFileName(_path_image);
            FileInfo f = new FileInfo(_path_image);
            f.CopyTo(temp, true);
            
        }
        //удаление пользователя из списка друзей
        public void DeleteUserFrend(int Id)
        {
            var user = context.Frends.ToList();
            foreach (var us in user)
            {
                if (us.IdFrend == Id || us.IdUser == Id)
                {
                    context.Frends.Remove(us);
                }
            }
            context.SaveChanges();
        }
    }
}
