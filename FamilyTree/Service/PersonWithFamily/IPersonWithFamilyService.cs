using FamilyTree.Model;
using FamilyTree.Model.PersonFamily;
using FamilyTree.Model.PersonWithFamily;

namespace FamilyTree.Service.PersonWithFamily
{
    public interface IPersonWithFamilyService
    {
        Task<ServiceResponseDTO> CreateAsync(PersonWithFamilyDTO dto);
        Task<ServiceResponseDTO> UpdateAsync(int personId, PersonWithFamilyDTO dto);
        Task<ServiceResponseDTO> DeleteAsync(int id);

        Task<ServiceResponseDTO<List<ListPersonWithFamilyDTO>>> GetAllAsync();
    }
}
