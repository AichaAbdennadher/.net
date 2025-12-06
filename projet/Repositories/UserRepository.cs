using metiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.Data;

namespace projet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        //public async Task<IEnumerable<User>> GetPharmaciens()
        //{
        //    return await context.users
        //        .Where(u => u.Role == Role.Pharmacien)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<User>> GetMedecinsExcept(int idMedecin)
        //{
        //    return await context.users
        //        .Where(u => u.Role == Role.Medecin && u.UserID != idMedecin)
        //        .ToListAsync();
        //}
    }
}
