namespace CleanArchitecture.Persistence.Context;

public class DataContext : DbContext, IUnitOfWork
{
    public DbSet<User> User { get; set; }

    private readonly IMediator _mediator;

    public DataContext(DbContextOptions<DataContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        //await _mediator.DispatchDomainEvents(this);
        _ = await base.SaveChangesAsync(cancellationToken);
        return true;
    }
}