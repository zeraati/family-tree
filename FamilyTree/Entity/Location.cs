using System.ComponentModel.DataAnnotations;

namespace FamilyTree.Entity
{
    public class Location
    {
        public Location(string title)
        {
            Title = title;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Person> Person { get; } = new List<Person>();
    }
}
