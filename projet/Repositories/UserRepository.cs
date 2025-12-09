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

        public async Task<IEnumerable<ApplicationUser>> GetMedecinsExcept(Guid idMedecin)
        {
            return await context.users
                .Where(u => u.UserRole == Role.Medecin && u.Id != idMedecin.ToString())
                .ToListAsync();
        }
    }
}
