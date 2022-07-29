using JWTAuth.Entities;

namespace JWTAuth.Data
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(AppEfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
