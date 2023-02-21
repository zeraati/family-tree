using FamilyTree.Model;

namespace FamilyTree.Service.User
{
    public interface IUserService
    {
        Task<ServiceResponseDTO<bool>> CheckUserAsync(string username, string pass);
    }
}
