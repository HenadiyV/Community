using Community.Domain.Abstract;
using Community.Domain.Concrete;
using Community.Domain.Entities;
using Community.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Community.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private IUserRepository user_repository;
        private IUserInfo info_repository;
        private IDescriptionRepository description_repository;
        private IUserSetingRepository userSeting_repository;
       // private IUserGroupRepository userGroup_repository;IUserGroupRepository userGroup_repo,
        private IAddressRepository address_repository;
        private IRoleRepository role_repository;
        private IFrendRepository frend_repository;
        private IGenderRepository gender_repository;
        private IPostRepository post_repository;
        public AdminController(IUserRepository repo, IUserInfo info_repo, IDescriptionRepository descript, 
            IUserSetingRepository userSeting_repo, IAddressRepository address_repo,
            IRoleRepository role_repo, IFrendRepository frend_repo, IGenderRepository gender_repo,
           IPostRepository post_repo)
        {
            user_repository = repo;
            info_repository = info_repo;
            description_repository = descript;
            userSeting_repository = userSeting_repo;
            address_repository = address_repo;
           // userGroup_repository = userGroup_repo;
            role_repository = role_repo;
            frend_repository = frend_repo;
            gender_repository = gender_repo;
            post_repository = post_repo;
        }
        public ActionResult Index()
        {
            ViewBag.user = user_repository.GetCountUsers();
            ViewBag.description = post_repository.NotReadPost(1);//description_repository.GetCountDescription();
            List<Coments> coment = post_repository.PostUser(1);
            UserInfo user = info_repository.GetUserInfo(1);
            ComplectUser complectUser = new ComplectUser { UserInfo = user, Coments = coment, countFrend = 0, countDescription = 0, countFrendBirthDay=0 };
            return View(coment);
        }
        public ActionResult ListUser()
        {
            return View(user_repository.Users.ToList());
        }
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = user_repository.GetUser((int)Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var address = address_repository.GetAddress((int)Id);
            if(address==null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? Id)
        {
            string catalog = Id.ToString();
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath(@"~/Files/User/" + catalog + "/"));
            if (directory.Exists)
            {
                Directory.Delete(directory.ToString(), true);
            }
            // userGroup_repository.GroupDelete(Id);
            description_repository.DeleteDescription((int)Id);
            post_repository.DeleteUserPost((int)Id);
            frend_repository.DeleteUserFrend((int)Id);
            userSeting_repository.SetingUserDelete((int)Id);
            address_repository.AddressDelete((int)Id);
            user_repository.UserDelete((int)Id);
            return RedirectToAction("ListUser");
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(SetingProfilUser model,DateTime BirthDay)
        {
            DirUser myDir = new DirUser();
           if (ModelState.IsValid)
            {
                user_repository.AddUser(model.User);
            int id = user_repository.GetIdNewUser();

                model.UserSeting.IdUser = id;
                model.User.BirthDay = BirthDay.ToShortDateString();
                model.UserSeting.Avatar = "no-photo.png";
                userSeting_repository.AddUserSeting(model.UserSeting);
                model.UserAddress.IdUser = id;
                address_repository.AddAddress(model.UserAddress);

                myDir.CreateNewDirectory(id);
                return RedirectToAction("Index", "Admin");
           }
             return View(model);
        }
        public ActionResult Details(int Id)
        {
            User user = user_repository.GetUser(Id);
            ViewBag.avatar = userSeting_repository.GetAvatar(Id);
            return View(user);
        }
          public ActionResult UserAddress(int Id)
                {
                    var userAddress = address_repository.Address.Where(us => us.IdUser == Id).FirstOrDefault();
                    User user=user_repository.GetUser(Id);
                    if (user != null) { 
                    ViewBag.userId = user.Id;
                    }
                    return View(userAddress);
                }
        [HttpPost]
        public ActionResult UserAddress(Address address)
        {
            if (ModelState.IsValid)
            {
                address_repository.SaveAddress(address);
                return RedirectToAction("Details"+address.IdUser);
            }
                return View(address);
        }
        public ActionResult UserSeting(int Id)
        {
            User user = user_repository.GetUser(Id);
            ViewBag.Name = user.Name + " " + user.FirstName + " " + user.Surname;
            UserSeting userSeting = userSeting_repository.GetUserSeting(Id);
           
            return View(userSeting);
        }
        [HttpPost]
        public ActionResult UserSeting(UserSeting userSeting)
        {
            if (ModelState.IsValid)
            {
                userSeting_repository.SaveSeting(userSeting);
                return RedirectToAction("Details/"+userSeting.IdUser, "Admin");
            }
            return View(userSeting);
        }
        public ActionResult Edit(int id)
        {
           
            User user = user_repository.GetUser(id);
        
          var role = role_repository.Roles;
            ViewBag.roly = role;
            var gender = gender_repository.Gender;
            ViewBag.gender = gender;
           
            ViewBag.userId = user.Id;
            ViewBag.userName = user.Name+" "+user.FirstName+" "+user.Surname;
           
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                user_repository.SaveUser(model);
             
                return RedirectToAction("ListUser");
            }
            
            return View(model);
        }
        public ActionResult UserDescription(int Id)
        {
           List< DescriptionUser> description = description_repository.GetDescriptionUsers(Id);
            User user = user_repository.GetUser(Id);
           
            ViewBag.userName = user.Name + " " + user.FirstName + " " + user.Surname;
            ViewBag.userId = user.Id;
            return View(description);
        }
        public ActionResult ListUserDescription(int Id,int IdDescription)
        {
           List<User> user = post_repository.GetListUserReadDescription(Id,IdDescription);
            ViewBag.Id = Id;
            return View(user);
        }
        public ActionResult AddDescriptionAdmin(int? a, int? Id)
        {
            HttpContext.Response.Cookies["status"].Value = a.ToString();
            HttpContext.Response.Cookies["privat"].Value = Id.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult AddDescriptionAdmin(string textBoxName)
        {
            int ID_privat = 0;
            string status = "";
            string name_file = "";
            DescriptionUser description = new DescriptionUser();
            
            description.IdUser = 1;
            HttpCookie cookieFile = Request.Cookies["NameFile"];
            if (cookieFile != null)
            {
                description.NameFile = cookieFile.Value;
                name_file = cookieFile.Value;
            }
            else description.NameFile = "";

            description.Date = DateTime.Now.Date.ToShortDateString();
            description.Description = textBoxName;
           
            description_repository.SaveDescription(description);
            int IdDescription = description_repository.DescriptionUsers.Max(a => a.IdDescription);
            HttpCookie cookieStatus = Request.Cookies["status"];
            if (cookieStatus != null)
            {
                status = cookieStatus.Value;
            }

            HttpCookie cookiePrivat = Request.Cookies["privat"];
            if (cookiePrivat != null)
            {
                int.TryParse(cookiePrivat.Value, out ID_privat);
            }
            if (status == "1")
            {
                List<int> Id_user = user_repository.Users.Select(a => a.Id).ToList();
                post_repository.SavePostList(1, Id_user, name_file, status);
                HttpContext.Response.Cookies["NameFile"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["status"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["privat"].Expires = DateTime.Now.AddDays(-1);
                return RedirectToAction("Index");
            }
            
            if (status == "3")
            {
                if (ID_privat > 0)
                {
                    post_repository.SavePost(1, ID_privat, name_file, status);
                }
                HttpContext.Response.Cookies["NameFile"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["status"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["privat"].Expires = DateTime.Now.AddDays(-1);
                return RedirectToAction("ListUser");
            }

            return View();
        }
        public ActionResult SearchAdmin()
        {
            DataNew listDate = new DataNew();
            List<string> city = address_repository.GetCity();
            ViewBag.city = new SelectList(city);
            List<string> date = listDate.SearchDate;
            ViewBag.listData = new SelectList(date);
            List<string> gender = new List<string>() { "Мужской", "Женский" };
            ViewBag.gender = new SelectList(gender);
          
            return View();
        }
        [HttpPost]
        public ActionResult SearchUserAdmin(string act, string name, string City, string Data, string stop,
           string surname, string firstname, string gender)

        {
            SearchUserResult searchResult = new SearchUserResult();
            List<SearchUserResult> ListResultSearch = new List<SearchUserResult>();

            if (act == "1" && City != null)
            {
                List<User> user = address_repository.GetSearchUserCity(City);//.Users.Where(us => us.Id == ID).FirstOrDefault();
                List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(user);
                ListResultSearch = searchResult.GetListSearchUserResult(user, usSeting);
            }
            if (act == "2" && name != null)
            {
                List<User> us = user_repository.GetSearchUser(name);
                List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(us);
                ListResultSearch = searchResult.GetListSearchUserResult(us, usSeting);
            }
            if (act == "3" && firstname != null)
            {
                List<User> user = user_repository.GetSearchUserFirstName(firstname);
                List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(user);
                ListResultSearch = searchResult.GetListSearchUserResult(user, usSeting);
            }
            if (act == "4" && surname != null)
            {
                List<User> user = user_repository.GetSearchUserSurName(surname);
                List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(user);
                ListResultSearch = searchResult.GetListSearchUserResult(user, usSeting);
            }
            if (act == "5" && gender != null)
            {
                List<User> user = user_repository.GetSearchUserGender(gender);
                List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(user);
                ListResultSearch = searchResult.GetListSearchUserResult(user, usSeting);
            }
            if (act == "6" && Data != null)
            {
                List<User> user = user_repository.GetUserDate(Data);
                List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(user);
                ListResultSearch = searchResult.GetListSearchUserResult(user, usSeting);
            }
            return View(ListResultSearch); 

        }
        [HttpPost]
        public ActionResult PipelSearchAdmin(string name)
        {
            List<User> us = user_repository.GetSearchUser(name);
            List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(us);
            SearchUserResult searchResult = new SearchUserResult();
            List<SearchUserResult> ListResultSearch = new List<SearchUserResult>();
            ListResultSearch = searchResult.GetListSearchUserResult(us, usSeting);
            if (ListResultSearch.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(us);
        }
        public ActionResult UserBan(int Id)
        {
            UserSeting userSeting = userSeting_repository.GetUserSeting(Id);
            return View(userSeting);
        }
        [HttpPost]
        public ActionResult UserBan(UserSeting userSeting)
        {
            if (ModelState.IsValid)
            {
                userSeting_repository.SaveSeting(userSeting);
                
                return RedirectToAction("ListUser");
            }

            return View(userSeting);
        }
    
    public JsonResult AjaxReadDescript(string idUs, string IdDescript)
    {
       int ID = User.Identity.GetUserId<int>();
        idUs = ID.ToString();
        post_repository.UserReadPost(idUs, IdDescript);
        return Json("Сервер получил данные: " + idUs + " - " + IdDescript);

    }
}
}
