using Community.Domain.Abstract;
using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Concrete
{
  public  class EFGenderRepository:IGenderRepository
    {
        //контекст данных
        EFDbContext context = new EFDbContext();
        //данные с тадлицы Genders
        public IEnumerable<Genders> Gender
        {
            get { return context.Gender; }
        }
        
        public int GetIdGender(string gender)
        {
            int Id_gender = 0;
            if(gender=="Мужской"){
                Id_gender = 1;
            }
            else  if (gender =="Женский"){
                Id_gender = 2;
            }
            return Id_gender;
        }
    }
}
