using metiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.DTO;

namespace projet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

      

        public async Task<IEnumerable<ApplicationUser>> GetPharmaciens()
        {
            return await context.users
                .Where(u => u.UserRole == Role.Pharmacien)
                .ToListAsync();
        }
    }
}
