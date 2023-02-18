using FamilyTree.Model.Enum;

namespace FamilyTree.Model.PersonFamily
{
    public class ListPersonWithFamilyDTO
    {
        public int PersonId { get; set; }
        public string? FullName { get; set; }
        public GenderEnum? GenderId { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string? Photo { get; set; }
        public string? BackgroundColor { get; set; }

        public int? FatherId { get; set; }
        public string? FatherFullName { get; set; }

        public int? MotherId { get; set; }
        public string? MotherFullName { get; set; }

        public List<int>? SpouseIds { get; set; }
    }
}
