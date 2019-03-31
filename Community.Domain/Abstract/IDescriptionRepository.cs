using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Community.Domain.Abstract
{
   public interface IDescriptionRepository
    {
        IEnumerable<DescriptionUser> DescriptionUsers { get; }
        string AddFileDescription(int ID, HttpPostedFileBase upload);
        void SaveDescription(DescriptionUser userDescription);
        List<DescriptionUser> GetDescriptionUsers(int Id);
       int GetCountDescriptionUser(int Id);
        string GetCountDescription();
        void DeleteDescription(int Id);
    }
}
