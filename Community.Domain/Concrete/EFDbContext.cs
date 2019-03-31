using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Community.Domain.Concrete
{
    //иницилизация базы
    public class Inisilization :DropCreateDatabaseAlways <EFDbContext>
    {
       
        //DropCreateDatabaseIfModelChanges
        protected override void Seed(EFDbContext context)
        {
            //адрес
            var AddressList = new List<Address>
            {
                new Address(){  Contry="Украина",City="Хмельницкий",Street="Свободы",      Hous="64", Apartment="34", IdUser=1},
                new Address(){  Contry="Украина",City="Хмельницкий",Street="Шевченко",     Hous="18", Apartment="" ,  IdUser=2},
                new Address(){  Contry="Украина",City="Киев",       Street="Академгородок",Hous="23", Apartment="123",IdUser=3},
                new Address(){  Contry="Украина",City="Киев",       Street="Азовская",     Hous="48", Apartment="86", IdUser=4 },
                new Address(){  Contry="Украина",City="Харьков",    Street="Володарского", Hous="67", Apartment="" ,  IdUser=5},
                new Address(){  Contry="Украина",City="Львов",      Street="Мазепы",       Hous="67", Apartment="" ,  IdUser=6},
                new Address(){  Contry="Украина",City="Ровно",      Street="Свободы",      Hous="28", Apartment="23", IdUser=7},
                new Address(){  Contry="Украина",City="Львов",      Street="Шевченко",     Hous="28", Apartment="" ,  IdUser=8},
                new Address(){  Contry="Украина",City="Киев",       Street="Академгородок",Hous="43", Apartment="1",  IdUser=9},
                new Address(){  Contry="Украина",City="Киев",       Street="Азовская",     Hous="56", Apartment="28", IdUser=10 },
                new Address(){  Contry="Украина",City="Харьков",    Street="Володарского", Hous="37", Apartment="12", IdUser=11},
                
            };
            AddressList.ForEach(s => context.Address.Add(s));
            context.SaveChanges();

            // пользователи
            var UserList = new List<User>
            {   new User(){  Name="Admin",  FirstName="",           Surname="",           Email="admin@test.com",   Phone="111-111-111", BirthDay="1987-04-12",RoleId=1,Password="Q1111q",GenderId=1, Uroven=777 },
                new User(){  Name="Василий",FirstName="Анатолиевич",Surname="Петров",     Email="petrov@test.com",  Phone="123-123-123", BirthDay="1987-04-12",RoleId=3,Password="A1111a",GenderId=1, Uroven=500 },
                new User(){  Name="Марина", FirstName="Андреевна",  Surname="Иванова",    Email="ivanova@test.com", Phone="234-234-234", BirthDay="1990-03-18",RoleId=3,Password="A1111a",GenderId=2, Uroven=500 },
                new User(){  Name="Иван",   FirstName="Степанович", Surname="Крузенштейн",Email="kyzja@test.com",   Phone="345-345-345", BirthDay="1988-07-10",RoleId=3,Password="A1111a",GenderId=1, Uroven=500 },
                new User(){  Name="Ольга",  FirstName="Васильевна", Surname="Смирнова",   Email="smirnova@test.com",Phone="456-456-456", BirthDay="1993-06-22",RoleId=3,Password="A1111a",GenderId=2, Uroven=500 },
                new User(){  Name="Петр",   FirstName="Николаевич", Surname="Рева",       Email="reva@test.com",    Phone="567-567-567", BirthDay="1985-08-24",RoleId=3,Password="A1111a",GenderId=1, Uroven=500 },
                new User(){  Name="Семен",  FirstName="Анатолиевич",Surname="Сидоров",    Email="sidorov@test.com", Phone="678-678-678", BirthDay="1979-04-12",RoleId=3,Password="A1111a",GenderId=1, Uroven=500 },
                new User(){  Name="Оксана", FirstName="Андреевна",  Surname="Романова",   Email="romanova@test.com",Phone="234-678-999", BirthDay="1996-03-18",RoleId=3,Password="A1111a",GenderId=2, Uroven=500 },
                new User(){  Name="Иван",   FirstName="Степанович", Surname="Витковский", Email="vita@test.com",    Phone="888-345-678", BirthDay="1981-07-10",RoleId=3,Password="A1111a",GenderId=1, Uroven=500 },
                new User(){  Name="Елена",  FirstName="Васильевна", Surname="Рац",        Email="rats@test.com",    Phone="456-678-567", BirthDay="1979-06-22",RoleId=3,Password="A1111a",GenderId=2, Uroven=500 },
                new User(){  Name="Николай",FirstName="Николаевич", Surname="Кохан",      Email="koxan@test.com",   Phone="678-567-456", BirthDay="1982-08-24",RoleId=3,Password="A1111a",GenderId=1, Uroven=500 }
            };
            UserList.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            //роли  
            var RoleList = new List<Role>
            {
                new Role(){ NameRole="admin" },
                new Role(){ NameRole="moderator" },
                new Role(){ NameRole="user" }
            };
            RoleList.ForEach(s => context.Rolies.Add(s));
            context.SaveChanges();

            //настройки отображения
            var UserSetingList = new List<UserSeting>
            {
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=1, GreateGroup=true,  Post=true,  UploadFile=true , AdminSeting=false, Avatar="no-photo.png"},
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=2, GreateGroup=false, Post=false, UploadFile=false, AdminSeting=false, Avatar="no-photo.png" },
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=3, GreateGroup=false, Post=false, UploadFile=false, AdminSeting=false, Avatar="no-photo.png" },
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=4, GreateGroup=false, Post=false, UploadFile=false, AdminSeting=false, Avatar="no-photo.png" },
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=5, GreateGroup=false, Post=false, UploadFile=false, AdminSeting=false, Avatar="no-photo.png" },
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=6, GreateGroup=true,  Post=false, UploadFile=true , AdminSeting=false, Avatar="no-photo.png"},
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=7, GreateGroup=false, Post=false, UploadFile=false, AdminSeting=false, Avatar="no-photo.png" },
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=8, GreateGroup=false, Post=false, UploadFile=false, AdminSeting=false, Avatar="no-photo.png" },
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=9, GreateGroup=false, Post=false, UploadFile=false, AdminSeting=false, Avatar="no-photo.png" },
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=10,GreateGroup=false, Post=false, UploadFile=false, AdminSeting=false, Avatar="no-photo.png" },
               new UserSeting(){ Age=true, Surname=true, Email=true,  Address=true, Phone=true, IdUser=11,GreateGroup=true,  Post=true,  UploadFile=true , AdminSeting=false, Avatar="no-photo.png"},

            };
            UserSetingList.ForEach(s => context.UserSetings.Add(s));
            context.SaveChanges();

            // групы (в проекте неиспользуеться)
            var GroupList = new List<Group>
             {
                 new Group(){  NameGroup="public" },
                 new Group(){  NameGroup="privat" }

             };
            GroupList.ForEach(s => context.Groups.Add(s));
            context.SaveChanges();

            // пользователи создавшие групы(в проекте неиспользуеться)
            var GroupUserList = new List<UserGroup>
             {
                 new UserGroup(){  IdUser=1,  IdGroup=1 },
                 new UserGroup(){  IdUser=1,  IdGroup=2 }

             };
            GroupUserList.ForEach(s => context.UserGroups.Add(s));
            context.SaveChanges();
            //сообщения
            var DescriptionList = new List<DescriptionUser>
             {
                 new DescriptionUser{ Date=DateTime.Now.Date.ToShortDateString(), Description="Мы рады приветствовать Вас в нашем сообществе!!!\n Здесь с уважением относяться к убеждениям и личным мнениям.\nТем не менее просим Вас придерживаться некоторым привилам:\n " +
                 "1.Запрещено оскарблять пользователей.\n" +
                 "2.Использовать нецензурные выражения.\n" +
                 "3.Спамить рекламой.  ",    IdUser=1, NameFile="" }
                
             };
            DescriptionList.ForEach(s => context.DescriptionUsers.Add(s));
            context.SaveChanges();
            //список отправителей и получателей сообщений
            var PostList = new List<Post>
             {
                 new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=2, UserRead=false},
                 new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=3, UserRead=false},
                 new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=4, UserRead=false},
                 new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=5, UserRead=false},
                 new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=6, UserRead=false},
                new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=7, UserRead=false},
                 new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=8, UserRead=false},
                 new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=9, UserRead=false},
                 new Post{ IdDescription=1, IdUserPublicPost=1, IdReadPost=10, UserRead=false}

             };
            PostList.ForEach(s => context.Posts.Add(s));
            context.SaveChanges();
            //список друзей
            var FrendList = new List<Frends>
             {
                 new Frends(){  IdUser=1,  IdFrend=2  },
                 new Frends(){  IdUser=2,  IdFrend=1  },
                 new Frends(){  IdUser=2, IdFrend=3 },
                  new Frends(){ IdUser=3, IdFrend=2 },
                  new Frends(){ IdUser=4, IdFrend=2 },
                  new Frends(){ IdUser=2, IdFrend=4 }

             };
            FrendList.ForEach(s => context.Frends.Add(s));
            context.SaveChanges();
            //гендерный список
            var GenderList = new List<Genders>
            {
                new Genders(){ Gender="мужщина " },
                new Genders(){ Gender="женщина " }
                
            };
            GenderList.ForEach(s => context.Gender.Add(s));
            context.SaveChanges();
        }
    }
    public class EFDbContext: DbContext
    {

        // иницилизация 
        //static EFDbContext()
        //{
        //    Database.SetInitializer<EFDbContext>(new Inisilization());
        //}

        public EFDbContext() : base("DefaultConection")
        { }
        public DbSet<User> Users{ get; set; }
        public DbSet<Address> Address { set; get; }
        public DbSet<Role> Rolies { get; set; }
        public DbSet<UserSeting> UserSetings { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Frends> Frends { get; set; }
        public DbSet<DescriptionUser> DescriptionUsers { get; set; }
        //public DbSet<UserFile> UserFiles { set; get; }
        //public DbSet<LogUser> LogUsers { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<Genders> Gender { set; get; }
    }
}
