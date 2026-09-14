
using Microsoft.Extensions.DependencyInjection;
using Oee.MarketAccess.Catalog;
using Oee.MarketAccess.Validation;
using QuickFix.FIX44;

namespace Oee.MarketAcess.Extensions;

public static class AddFixGatewayExtension
{
    public static IServiceCollection AddFixGateway(this IServiceCollection services)
    {
        var catalog = new InMemoryInstrumentCatalog(SyntheticInstrumentSeed.CreateProfiles());
        services.AddSingleton<IInstrumentCatalog>(catalog);
        services.AddSingleton<IValidator<Message>, Validator>();
        services.AddSingleton<FixApplication>();
        services.AddHostedService<FixGateway>(); // FixGateway is ONLY a background service and never injected elsewhere.

        return services;
    }
}