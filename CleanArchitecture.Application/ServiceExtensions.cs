using CleanArchitecture.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


    }

    //reference: https://github.com/hgmauri/sample-cqrs-mediatr
    public static void AddMassTransitExtension(this IServiceCollection services)
    {
        //services.AddMassTransit(x =>
        //{
        //    x.AddDelayedMessageScheduler();
        //    x.SetKebabCaseEndpointNameFormatter();

        //    x.AddConsumer<SendEmailConsumerHandler>(typeof(SendEmailConsumerHandlerDefinition));

        //    x.UsingInMemory((ctx, cfg) =>
        //    {
        //        cfg.UseDelayedMessageScheduler();
        //        cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
        //        cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
        //    });
        //});
    }

  
}