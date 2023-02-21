using FamilyTree.Model;
using FamilyTree.Helper;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Service.User
{
    public class UserService : IUserService
    {
        private DataContext _context;
        public UserService(DataContext context, IWebHostEnvironment webHostEnvironment)=> _context = context;

        public async Task<ServiceResponseDTO<bool>> CheckUserAsync(string username, string pass)
        {
            var checkUser = await _context.User.AnyAsync(x => x.UserName == username && x.Pass==pass);
            return new ServiceResponseDTO<bool>(checkUser);
        }
    }
}
