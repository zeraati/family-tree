namespace FamilyTree.Entity
{
    public class PersonSpouse
    {
        public PersonSpouse(int personId,int spouseId)
        {
            PersonId = personId;
            SpouseId = spouseId;
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public int SpouseId { get; set; }

        public virtual Person? Person { get; set; }
        public virtual Person? Spouse { get; set; }
    }
}
