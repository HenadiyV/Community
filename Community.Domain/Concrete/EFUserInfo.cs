using Community.Domain.Abstract;
using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Concrete
{
  public  class EFUserInfo:IUserInfo
    {
        // контекст данных
        EFDbContext context = new EFDbContext();
        //как вариант сбора по  таблицам данных о пользователе
        public UserInfo GetUserInfo(int id)
        {
            DataNew NewDate = new DataNew();
            DataNew BirthDate = new DataNew();
            DataNew NewDate1 = new DataNew();
            DataNew BirthDate1 = new DataNew();
            string date = DateTime.Now.ToShortDateString();
            var user = context.Users.Where(us => us.Id == id).FirstOrDefault();
            string userData = user.BirthDay;
            int age = 0;
            if (userData != "") { 
            NewDate1=NewDate.UserNewData(date);
            BirthDate1=BirthDate.UserBirthDay(userData);
                if(BirthDate1!= null&& NewDate1 != null)
             age = (BirthDate1.year - NewDate1.year)*-1;
            }
            string myAge = "";
            myAge = UserAge(age);
            var usSeting = context.UserSetings.Where(set => set.IdUser == id).FirstOrDefault();

            UserInfo userInfo = new UserInfo() { _IdUser = user.Id, _Name = user.Name, _FirstName = user.FirstName,
                _Surname = user.Surname, _BirthDay = userData, _Email = user.Email, _Phone = user.Phone,
                _AddressSeting = usSeting.Address, _EmailSeting = usSeting.Email, _PhoneSeting = usSeting.Phone, _SurnameSeting = usSeting.Surname,
                _AgeSeting = usSeting.Age, _Age = myAge, _Avatar=usSeting.Avatar };

            return userInfo;
           
        }
        // возраст
        public string UserAge(int age)
        {
            int temp = 0;
            string myAge = "";
            if (age > 21)
            {
                temp = age % 10;
            }
            else temp = age;
            switch (temp)
            {
                case 0: myAge = " Дата рождения не указана."; break;
                case 1: myAge = age.ToString() + "  год "; break;
                case 2: myAge = age.ToString() + "  года "; break;
                case 3: myAge = age.ToString() + "  года "; break;
                case 4: myAge = age.ToString() + "  года "; break;
                default: myAge = age.ToString() + "  лет "; break;
            }
            return myAge;
        }
    }
}