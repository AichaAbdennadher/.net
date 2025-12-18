using metiers;
using projet.Data;

namespace projet.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetPharmaciens();

    }
}
