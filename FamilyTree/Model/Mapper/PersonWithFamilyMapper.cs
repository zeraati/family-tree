using FamilyTree.Entity;
using System.Linq.Expressions;
using FamilyTree.Model.PersonFamily;

namespace FamilyTree.Model.Mapper
{
    public static class PersonWithFamilyMapper
    {
        public static Expression<Func<Person, ListPersonWithFamilyDTO>> MapList
        {
            get
            {
                return x => new ListPersonWithFamilyDTO
                {
                    PersonId = x.Id,
                    FullName = x.FirstName + " " + x.LastName,
                    GenderId = x.GenderId,
                    Gender = x.GenderId.ToString(),
                    Photo=x.Photo,
                    FatherId = x.PersonFamily != null ? x.PersonFamily.FatherId : null,
                    FatherFullName = x.PersonFamily != null && x.PersonFamily.Father != null ? x.PersonFamily.Father.FirstName + " " + x.PersonFamily.Father.LastName : null,
                    MotherId = x.PersonFamily != null ? x.PersonFamily.MotherId : null,
                    MotherFullName = x.PersonFamily != null && x.PersonFamily.Mother != null ? x.PersonFamily.Mother.FirstName + " " + x.PersonFamily.Mother.LastName : null,
                    SpouseIds = x.PersonSpouse != null ? x.PersonSpouse.Select(y => y.SpouseId).ToList() : null
                };
            }
        }
    }
}
