
namespace CleanArchitecture.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public void Create(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Get(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return _context.User.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
       return _context.SaveChangesAsync(cancellationToken);
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }
}