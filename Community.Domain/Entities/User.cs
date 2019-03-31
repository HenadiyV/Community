using Community.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
    public class User: IValidatableObject

    {
        [Key]
        public int Id { set; get; }
        [Display(Name = "Фамилия")]
        public string Surname { set; get; }
        [Display(Name = "Имя")]
        public string Name { set; get; }
        [Display(Name = "Отчество")]
        public string FirstName { set; get; }
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        public string Phone { set; get; }
        public string BirthDay { set; get; }
        [DataType(DataType.Password)]
        public string Password { set; get; }
        public int  Uroven { set; get; }
        public int RoleId { set; get; }
        public virtual Role URoles { set; get; }
        public int GenderId { set; get; }
        public virtual Genders UGender {set;get;}
        
        bool res = true;
        // валидация
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            List<ValidationResult> errors = new List<ValidationResult>();
if (res) { 
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                errors.Add(new ValidationResult("Поле имя незаполненно"));
            }else 
                if ((this.Name.Length<3))
                {
                    errors.Add(new ValidationResult("Слишком короткое имя "));
               }else

                if ((this.Name.Length > 10))
                {
                    errors.Add(new ValidationResult("Слишком длиное имя "));
                }
                if (string.IsNullOrWhiteSpace(this.Email))
            {
                errors.Add(new ValidationResult("Поле email незаполненно"));
            }else
            if (!ValidEmail(this.Email))
            {
                errors.Add(new ValidationResult("Введите коректный email адрес"));
            }else
            if (!UserEmail(this.Id,this.Email))
                {
                    errors.Add(new ValidationResult("Данный email уже зарегистрирован "));
                }
                if (string.IsNullOrWhiteSpace(this.Password))
            {
                errors.Add(new ValidationResult("Поле пароль незаполненно"));
            }else
                if (this.Password.Length < 5)
                {
                    errors.Add(new ValidationResult("Пароль минимум шесть символов"));
                }else
                if (!ValidPassword(this.Password))
                {
                    errors.Add(new ValidationResult("Пароль должен состоять из латинских букв верхнего и нижнего регистра и цифр"));
                }
                res = !res;
            
            }
            return errors;
        }
        //валидация email
        static bool ValidEmail(string email)
        {
            if (email != null) { 
            int sobaka = email.IndexOf("@");
            int tochka = email.IndexOf(".");

           if(sobaka>2&&tochka>4)
            {
                return true;
            }
            }
                return false;
        }
        // валидация пароля
        static bool ValidPassword(string password)
        {
            string upList = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
            string lowList = "abcdefghijklmnoprstuvwxyz";
            string numericList = "1234567890";
            return (password.IndexOfAny(upList.ToCharArray()) > -1)
            && (password.IndexOfAny(lowList.ToCharArray()) > -1)
            && (password.IndexOfAny(numericList.ToCharArray()) > -1);
        }
        // проверка дубля email 
        public bool UserEmail(int Id,string email)
        {
            EFDbContext context = new EFDbContext();
            if (email != null && Id==0)
            {
                var eml = context.Users.Where(s => s.Email == email).FirstOrDefault();
                if (eml == null)
                {
                    return true;
                }
            }else if (Id > 0)
            {
                var us = context.Users.Where(s => s.Email == email).FirstOrDefault();
                if (us.Id != Id)
                {
                    return false;
                }
                else { return true; }
            }
            return false;
        }
    }
}
