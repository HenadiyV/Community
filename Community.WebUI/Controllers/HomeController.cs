using Community.Domain.Abstract;
using Community.Domain.Entities;
using Community.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community.WebUI.Controllers
{
    public class HomeController : Controller
    {
        int ID = 0;
        private IUserRepository user_repository;
        private IUserInfo info_repository;
        private IDescriptionRepository description_repository;
        private IUserSetingRepository userSeting_repository;
        private IAddressRepository address_repository;
        private IFrendRepository frend_repository;
        private IPostRepository post_repository;
        private IGenderRepository gender_repository;
        public HomeController(IUserRepository repo, IUserInfo info_repo, IDescriptionRepository descript,
            IUserSetingRepository userSeting_repo, IAddressRepository address_repo, IFrendRepository frend_repo, IPostRepository post_repo, IGenderRepository gender_repo)
        {
            user_repository = repo;
            info_repository = info_repo;
            description_repository = descript;
            userSeting_repository = userSeting_repo;
            address_repository = address_repo;
            frend_repository = frend_repo;
            post_repository = post_repo;
            gender_repository = gender_repo;
        }
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
        //[Authorize]
        public ActionResult PageUser()
        {
            ID = User.Identity.GetUserId<int>();
            int countFrends = frend_repository.GetCountFrend(ID);
            int countDescriptions = post_repository.NotReadPost(ID);
            int countFrendBirthDay = frend_repository.GetCountFrendBithDay(ID);
            List<Coments> coment = post_repository.PostUser(ID);
            UserInfo user = info_repository.GetUserInfo(ID);
            ComplectUser complectUser = new ComplectUser { UserInfo = user, Coments = coment, countFrend = countFrends, countDescription = countDescriptions, countFrendBirthDay = countFrendBirthDay };
            ViewBag.user = ID;
            return View(complectUser);
        }
        public ActionResult ViewUser(int Id)
        {
            int countDescript = description_repository.GetCountDescriptionUser(Id);
            ViewBag.counDescription = countDescript;
            int countFrends = frend_repository.GetCountFrend(Id);
            ViewBag.counFrend = countFrends;

            int countFrendBirthday = frend_repository.GetCountFrendBithDay(Id);
            ViewBag.countFrendBithDay = countFrendBirthday;
            var userView = user_repository.GetUser(Id);
            List<Coments> coment = post_repository.PostUser(Id);
            UserInfo user = info_repository.GetUserInfo(Id);
            ComplectUser complectUser = new ComplectUser { UserInfo = user, Coments = coment, countDescription = countDescript, countFrend = countFrends, countFrendBirthDay = countFrendBirthday };
            return View(complectUser);
        }
        public ActionResult ListUserHome()
        {
            ID = User.Identity.GetUserId<int>();
            SearchUserResult searchResult = new SearchUserResult();
            List<User> usFrend = frend_repository.GetFrends(ID);
            List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(usFrend);
            List<SearchUserResult> ListResultSearch = new List<SearchUserResult>();
            ListResultSearch = searchResult.GetListSearchUserResult(usFrend, usSeting);
            if (ListResultSearch != null)
            {
                return View(ListResultSearch);
            }
            else return View();
        }
        #region Profil
        public ActionResult Profil()
        {
            ID = User.Identity.GetUserId<int>();
            User user = user_repository.GetUser(ID);
            Address address = address_repository.GetAddress(ID);
            var gender = gender_repository.Gender;
            ViewBag.gender = gender;
            ProfilUser profilUser = new ProfilUser() { User = user, UserAddress = address };
            return View(profilUser);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profil(ProfilUser us)
        {
            if (ModelState.IsValid)
            {
                if (!user_repository.SaveUser(us.User))
                {
                    ViewBag.mess = "Email in corect!!!";
                    var gender = gender_repository.Gender;
                    ViewBag.gender = gender;
                    return View(us);
                }
                address_repository.SaveAddress(us.UserAddress);
                return RedirectToAction("PageUser/" + us.User.Id);
            }

            return View();

        }
        #endregion
        #region Seting
        public ActionResult Seting()
        {
            ID = User.Identity.GetUserId<int>();
            UserSeting usSeting = userSeting_repository.UserSetings.Where(us => us.IdUser == ID).FirstOrDefault();
            return View(usSeting);
        }
        [HttpPost]
        public ActionResult Seting(UserSeting userSeting)
        {
            if (ModelState.IsValid)
            {
                userSeting_repository.SaveSeting(userSeting);
                return RedirectToAction("PageUser/" + userSeting.IdUser);
            }
            return View(userSeting);
        }
        [HttpPost]
        public ActionResult AddAvatar(HttpPostedFileBase upload)
        {
            ID = User.Identity.GetUserId<int>();
            userSeting_repository.UserAvatar(ID, upload);
            return RedirectToAction("Seting/" + ID);
        }

        #endregion
        #region Description
        public ActionResult AddDescription(int? a, int? Id)
        {
            ID = User.Identity.GetUserId<int>();
            ViewBag.options = user_repository.GetUroven(ID);
            HttpContext.Response.Cookies["status"].Value = a.ToString();
            HttpContext.Response.Cookies["privat"].Value = Id.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult AddDescription(string textBoxName)
        {
            int ID_privat = 0;
            string status = "";
            string name_file = "";

            DescriptionUser description = new DescriptionUser();
            ID = User.Identity.GetUserId<int>();
            UserSeting userSeting = userSeting_repository.GetUserSeting(ID);
            if (userSeting.AdminSeting)
            {
                description.IdUser = ID;
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
                    if (userSeting.Post)
                    {
                        List<int> Id_user = user_repository.Users.Select(a => a.Id).ToList();
                        post_repository.SavePostList(ID, Id_user, name_file, status);
                    }
                    else
                    {

                        return RedirectToAction("NotPublicPost");
                    }
                }
                if (status == "2")
                {
                    if (userSeting.Post)
                    {
                        List<int> frend = frend_repository.Frends.Select(a => a.IdFrend).ToList();
                        post_repository.SavePostList(ID, frend, name_file, status);
                    }
                    else
                    {

                        return RedirectToAction("NotPublicPost");
                    }

                }

                if (status == "3" && userSeting.Post)
                {
                    if (userSeting.Post)
                    {
                        if (ID_privat > 0)
                        {
                            post_repository.SavePost(ID, ID_privat, name_file, status);
                        }
                    }
                    else
                    {

                        return RedirectToAction("NotPublicPost");
                    }
                }

                if (status == "4" && userSeting.Post)
                {
                    post_repository.SavePost(ID, 1, name_file, status);

                }

                HttpContext.Response.Cookies["NameFile"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["status"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["privat"].Expires = DateTime.Now.AddDays(-1);
                return RedirectToAction("PageUser/" + ID);
            }
            return RedirectToAction("NotPublicPost");
        }
        public ActionResult NotPublicPost()
        {
            string message = "";
            ID = User.Identity.GetUserId<int>();
            UserSeting us = userSeting_repository.GetUserSeting(ID);
            if (!us.AdminSeting)
            {
                message += " Обратитесь к администратору. ";
                ViewBag.profil = false;
            }
            else
            if (!us.Post)
            {
                message += " Завершить регистрацию ";
                ViewBag.profil = true;
            }
            else
            if (!us.UploadFile)
            {
                message += " Добавте адрес. ";
                ViewBag.profil = true;
            }
            ViewBag.Descript = " Для того чтобы использовать все возможности сервиса. ";
            ViewBag.message = message;
            return View();
        }
        [HttpPost]
        public JsonResult Upload()
        {
            ID = User.Identity.GetUserId<int>();
            UserSeting userSeting = userSeting_repository.GetUserSeting(ID);
            if (userSeting.AdminSeting)
            {
                if (userSeting.UploadFile)
                {
                    string catalog = ID.ToString() + "/";
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i]; //Uploaded file 
                                                                    //Use the following properties to get file's name, size and MIMEType 
                        int fileSize = file.ContentLength;
                        string fileName = file.FileName;
                        if (fileName != "")
                        {
                            HttpContext.Response.Cookies["NameFile"].Value = fileName;
                        }
                        else
                        {
                            HttpContext.Response.Cookies["NameFile"].Value = "";
                        }
                        string mimeType = file.ContentType;
                        System.IO.Stream fileContent = file.InputStream;
                        //To save file, use SaveAs method 
                        file.SaveAs(Server.MapPath("~/Files/User/") + catalog + "upload/" + fileName); //File will be saved in application root 
                    }
                    return Json("Файл добавлен. ");
                }
            }

            return Json("Опция недоступна.Завершите регистрацию.Или обратитесь к администратору.");
        }
        [HttpPost]
        public JsonResult AjaxReadDescript(string idUs, string IdDescript)
        {
            ID = User.Identity.GetUserId<int>();
            idUs = ID.ToString();
            post_repository.UserReadPost(idUs, IdDescript);
            return Json("Сервер получил данные: " + idUs + " - " + IdDescript);
        }
        #endregion
        #region Frend 
        [HttpPost]
        public JsonResult AddFrend(int Id_user)
        {
            ID = User.Identity.GetUserId<int>();
            string NameUser = user_repository.GetUserFIO(Id_user);
            if (frend_repository.AddFrend(ID, Id_user))
            {
                return Json(NameUser + "  Добавлен в список друзей.");

            }
            else
                return Json("Ошибка при добавление  в список.");

        }
        public ActionResult RemoveFrend()
        {
            ID = User.Identity.GetUserId<int>();
            SearchUserResult searchResult = new SearchUserResult();
            List<User> usFrend = frend_repository.GetFrends(ID);
            List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(usFrend);
            List<SearchUserResult> ListResultSearch = new List<SearchUserResult>();
            ListResultSearch = searchResult.GetListSearchUserResult(usFrend, usSeting);
            return View(usFrend);
        }
        [HttpPost]
        public JsonResult RemoveFrend(int Id)
        {
            ID = User.Identity.GetUserId<int>();
            string NameFrend = frend_repository.PersonaFrend(ID, Id);
            if (frend_repository.FrendRemove(ID, Id))
            {

                return Json(NameFrend + "  Удален из списока друзей.");

            }
            else
                return Json("Ошибка при удалении из списка.");
        }
        public ActionResult FrendBithDay()
        {
            ID = User.Identity.GetUserId<int>();

            List<MyFrend> usFrend = frend_repository.GetCountDay(ID);

            if (usFrend.Count <= 0)
            {
                return HttpNotFound();
            }
            return View(usFrend);
        }
        #endregion
        #region Search
        [HttpPost]
        public ActionResult PipelSearch(string name)
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
        public ActionResult Search()
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
        public ActionResult SearchUser(string act, string name, string City, string Data, string stop,
           string surname, string firstname, string gender)

        {
            SearchUserResult searchResult = new SearchUserResult();
            List<SearchUserResult> ListResultSearch = new List<SearchUserResult>();

            if (act == "1" && City != null)
            {
                List<User> user = address_repository.GetSearchUserCity(City);
                List<UserSeting> usSeting = userSeting_repository.GetUserSearchUserSeting(user);
                ListResultSearch = searchResult.GetListSearchUserResult(user, usSeting);
            }
            if (act == "2" && name != null)
            {
                List<User> us = user_repository.GetSearchUserName(name);
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
        #endregion

    }
}
