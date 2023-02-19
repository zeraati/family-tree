using FamilyTree.Model.Enum;

namespace FamilyTree.Entity
{
    public class Person
    {
        public Person(string firstName, string lastName, GenderEnum genderId)
        {
            FirstName = firstName;
            LastName = lastName;
            GenderId = genderId;
        }

        public Person(string firstName, string lastName, GenderEnum genderId,DateTime? birthDate, DateTime? deathDate,
            string? backgroundColor,string? description)
        {
            FirstName = firstName;
            LastName = lastName;
            GenderId = genderId;
            BirthDate = birthDate;
            DeathDate = deathDate;
            BackgroundColor = backgroundColor;
            Description = description;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum GenderId { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string? Phone{ get; set; }
        public int? LocationId { get; set; }
        public int? JobId { get; set; }
        public string? Note { get; set; }
        public string? Photo { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Description { get; set; }

        public virtual Job? Job { get; set; }
        public virtual Location? Location { get; set; }
        public virtual PersonFamily? PersonFamily { get; set; }
        public virtual ICollection<PersonFamily> FatherOfPersons { get; } = new List<PersonFamily>();
        public virtual ICollection<PersonFamily> MotherOfPersons { get; } = new List<PersonFamily>();

        public virtual ICollection<PersonSpouse> PersonSpouse { get; } = new List<PersonSpouse>();
        public virtual ICollection<PersonSpouse> SpousePerson { get; } = new List<PersonSpouse>();

    }
}
