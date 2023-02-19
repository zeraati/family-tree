using FamilyTree.Model.Enum;

namespace FamilyTree.Model.PersonWithFamily
{
    public class PersonWithFamilyDTO
    {
        public PersonWithFamilyDTO(string firsrtName, string lastName, GenderEnum genderId)
        {
            FirsrtName = firsrtName;
            LastName = lastName;
            GenderId = genderId;
        }

        public string FirsrtName { get; set; }
        public string LastName { get; set; }
        public GenderEnum GenderId { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate{get; set;}

        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public List<int>? SpouseIds { get; set; }

        public List<int>? ChildrenIds { get; set; }

        public string? BackgroundColor { get; set; }
        public string? Description { get; set; }
    }
}
