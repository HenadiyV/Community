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
    public class EFAddressRepository : IAddressRepository
    {
        // контекст данных
        EFDbContext context = new EFDbContext();
        //список адресов
        public IEnumerable<Address> Address
        {
            get { return context.Address; }
        }
        //сохраняем 
        public void SaveAddress(Address address)
        {
            context.Entry(address).State = EntityState.Modified;
            context.SaveChanges();
            UserRegistrationAddress(address);
        }
        //если есть адрес, разрешить отправку файлов
        private void UserRegistrationAddress(Address address)
        {
            if (address.Contry != "" && address.City!= "" && address.Street != "" && address.Hous != "")
            {
                UserSeting usSeting = context.UserSetings.Find(address.IdUser);
                if (usSeting != null)
                {
                    usSeting.UploadFile = true;
                    context.Entry(usSeting).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
        //удаление адреса
        public void AddressDelete(int Id)
        {
            var address = context.Address.Find(Id);
            if (address != null)
            {
                context.Address.Remove(address);
                context.SaveChanges();
            }
        }
        //добавление адреса
        public void AddAddress(Address address)
        {
            context.Address.Add(address);
            context.SaveChanges();
        }
        //возвращаем адрес  определеного пользователя
        public Address GetAddress(int Id)
        {
            Address address = context.Address.Where(a => a.IdUser == Id).FirstOrDefault();

            return address;
        }
        //заполнения полей таблицы адрес при создании пользователя
        public void DefaultUserAddress(int Id)
        {
            Address usAddress = new Address();
            usAddress.Contry = "";
            usAddress.City = "";
            usAddress.Street = "";
            usAddress.Hous = "";
            usAddress.Apartment = "";
            usAddress.Hous = "";
            usAddress.IdUser =Id;
            context.Address.Add(usAddress);
            context.SaveChanges();


        }
        //список городов зарегистрированных пользователей
        public List<string> GetCity()
        {
            List<string> myCity = new List<string>();
           
            var address = context.Address.ToList();
            foreach (var cit in address)
            {
                string my_city = cit.City;
                myCity.Add(my_city);
            } 
           List<string> myCityTemp = new List<string>(myCity.Distinct());
            return myCityTemp;
        }
        //список пользователей с определеного города
        public List<User> GetSearchUserCity(string city)
        {
            List<User> IdUserList = new List<User>();
            var cit = context.Address.Where(a => a.City==city).ToList();
            foreach(var c in cit)
            {
                User us = new User();
                us = context.Users.Where(a => a.Id == c.IdUser).FirstOrDefault();
                IdUserList.Add(us);
            }
            return IdUserList;
        }
    }
}
