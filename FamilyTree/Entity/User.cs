using System.ComponentModel.DataAnnotations;

namespace FamilyTree.Entity
{
    public class User
    {
        public User(string userName,string pass)
        {
            UserName = userName;
            Pass = pass;
        }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Pass { get; set; }
    }
}
