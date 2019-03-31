using Community.Domain.Concrete;
using Community.Domain.Entities;
using Community.Domain.ViewModel;
using Microsoft.Owin.Security;
using System.Web.Optimization;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Community.Domain.Abstract;
using System.Data.Entity.Validation;

namespace Community.WebUI.Controllers
{
    public class AccountController : Controller
    {
        EFDbContext db = new EFDbContext();
        private IUserRepository user_repository;
        private IAddressRepository address_repository;
        private IUserSetingRepository userSeting_repository;
        private IGenderRepository gender_repository;
        private IPostRepository post_repository;
        public AccountController(IUserRepository repo, IUserSetingRepository userSeting_repo, IAddressRepository address_repo, IGenderRepository gender_repo, IPostRepository post_repo)
        {
            user_repository = repo;
            userSeting_repository = userSeting_repo;
            address_repository = address_repo;
            gender_repository = gender_repo;
            post_repository = post_repo;
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
           
            if (ModelState.IsValid)
            {
               User user = await db.Users.Include(u => u.URoles).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    if (user.URoles != null)
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.URoles.NameRole, ClaimValueTypes.String));

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if ( user.Id==1&&user.RoleId==1)//
                    { return RedirectToAction("index","Admin"); }
                    return RedirectToAction("PageUser", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
           List<Genders>  gender = gender_repository.Gender.ToList();
            ViewBag.gender = gender;
            User us = new User();
            return View();
        }
        
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
     [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,Name,Email,Password,GenderId")] User model)
        {
            DirUser myDir = new DirUser();
            if (model.Email != null) { 
           
           
            if (ModelState.IsValid)
            {
                    
                       
                        var user = new User { Name = model.Name, Email = model.Email, GenderId=model.GenderId/*,, Uroven=500  FirstName="", Surname="", BirthDay="", Phone=""*/, RoleId=3, Password=model.Password};
                db.Users.Add(user);
                db.SaveChanges();

                
                   
                int id = user_repository.GetIdNewUser();
                
                userSeting_repository.DefaultUserSeting(id);
               
                address_repository.DefaultUserAddress(id);
                        post_repository.DefaultPost(id);
                myDir.CreateNewDirectory(id);
                    ViewBag.Welcome = "Теперь Вы можете войти под своей регистрационной записью.";
                return RedirectToAction("Index", "Home");
            }
             
            }
            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }
    }
}
