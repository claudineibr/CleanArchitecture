using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitecture.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{

    public AuditableEntitySaveChangesInterceptor()
    {
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public static void UpdateEntities(DbContext context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                //entry.Entity.CreatedBy = _identityService.GetPersonName();
                entry.Entity.DateCreated = DateTime.UtcNow;
                continue;
            }

            if (entry.State == EntityState.Modified)
            {
                //entry.Entity.LastModifiedBy = _identityService.GetPersonName();
                entry.Entity.DateUpdated = DateTime.UtcNow;
                continue;
            }

            if (entry.State == EntityState.Deleted)
            {
                //entry.Entity.DeletedBy = _identityService.GetPersonName();
                entry.Entity.DateDeleted = DateTime.UtcNow;
                entry.State = EntityState.Modified;
            }
        }
    }
}

