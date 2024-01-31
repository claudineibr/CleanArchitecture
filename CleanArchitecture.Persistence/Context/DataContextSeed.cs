namespace CleanArchitecture.Persistence.Context;

public class DataContextSeed
{
    public async Task SeedAsync(DataContext context, ILogger<DataContextSeed> logger)
    {
        if (context.Database.IsNpgsql())
        {
            var policy = CreatePolicy(logger, nameof(DataContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                // SeedAsync
            });
        }
    }

    private static AsyncRetryPolicy CreatePolicy(ILogger<DataContextSeed> logger, string prefix, int retries = 3)
    {
        return Policy.Handle<Exception>().
            WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(
                        exception,
                        "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}",
                        prefix,
                        exception.GetType().Name,
                        exception.Message,
                        retry,
                        retries
                        );
                }
            );
    }
}
