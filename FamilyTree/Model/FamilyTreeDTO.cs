namespace FamilyTree.Model
{
    public class PersonFamilyTreeDTO
    {
        public PersonFamilyTreeDTO(int id,string name, string gender)
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
        }

        public int id { get; set; }
        public List<int>? pids { get; set; }
        public int? mid { get; set; }
        public int? fid { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
    }
}
