
using System.ComponentModel.DataAnnotations;

namespace FamilyTree.Entity
{
    public class PersonFamily
    {
        public PersonFamily(int personId)
        {
            PersonId = personId;
        }

        [Required]
        public int PersonId { get; set; }
        public int? FatherId { get; set; }
        public int? MotherId { get; set; }

        public virtual Person? Person { get; set; }
        public virtual Person? Father { get; set; }
        public virtual Person? Mother { get; set; }
    }
}
