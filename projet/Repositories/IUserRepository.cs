using metiers;
using projet.Data;

namespace projet.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetPharmaciens();
        Task<IEnumerable<ApplicationUser>> GetMedecinsExcept(Guid idMedecin);

    }
}
