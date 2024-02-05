using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Repositories;

public interface IUserRepository : IApplicationDbContext
{
    void Create(User entity);
    void Update(User entity);
    void Delete(User entity);
    Task<User> Get(Guid id, CancellationToken cancellationToken);
    Task<List<User>> GetAll(CancellationToken cancellationToken);
    Task<User> GetByEmail(string email, CancellationToken cancellationToken);
}