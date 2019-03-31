using System.ComponentModel.DataAnnotations;

namespace Community.Domain.Entities
{
    public class DescriptionUser
    {
        [Key]
        public int IdDescription { set; get; }
        public int IdUser { set; get; }
        public string Description { set; get; }
        public string NameFile { set; get; }
        public string Date { set; get; }
    }
}
