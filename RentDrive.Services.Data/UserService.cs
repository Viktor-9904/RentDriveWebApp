using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;

namespace RentDrive.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser, Guid> userRepository;
        public UserService(IRepository<ApplicationUser, Guid> userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<string> GetOwnerNameById(Guid? id)
        {
            string? ownerName = await this.userRepository
                .GetAllAsQueryable()
                .Where(u => u.Id == id)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

            return ownerName ?? string.Empty;
        }
    }
}
