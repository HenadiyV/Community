using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
    //в проекте не используеться
   public class UserGroup
    {
        [Key]
        public int Id { set; get; }
        public int IdUser { set; get; }
        public int IdModerator { set; get; }
        public int IdGroup { set; get; }
        public ICollection<User> GUsers { get; set; }
        public UserGroup()
        {
            GUsers = new List<User>();
        }
    }
}
