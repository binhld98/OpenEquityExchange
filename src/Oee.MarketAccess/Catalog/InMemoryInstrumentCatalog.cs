namespace Oee.MarketAccess.Catalog;

/// <summary>
/// Provides case-insensitive instrument lookup over a fixed in-memory snapshot.
/// Copies supplied profiles during construction and supports concurrent reads.
/// </summary>
public class InMemoryInstrumentCatalog : IInstrumentCatalog
{   
    private readonly IDictionary<string, SimpleInstrumentProfile> _profiles;

    /// <summary>
    /// Create a catalog, rejecting null input, default profiles, and dupplicate symbols or IDs
    /// </summary>
    /// <param name="profiles"></param>
    /// <exception cref="ArgumentException"></exception>
    public InMemoryInstrumentCatalog(params SimpleInstrumentProfile[] profiles)
    {
        ArgumentNullException.ThrowIfNull(profiles);

        _profiles = new Dictionary<string, SimpleInstrumentProfile>(StringComparer.OrdinalIgnoreCase);
        var instrumentIds = new HashSet<long>();

        foreach (var profile in profiles)
        {
            if (profile == default)
                throw new ArgumentException("Instrument profiles must not contain default values.", nameof(profiles));
            
            if (!_profiles.TryAdd(profile.Symbol, profile))
                throw new ArgumentException($"Duplicate instrument symbol '{profile.Symbol}'.", nameof(profiles));

            if (!instrumentIds.Add(profile.InstrumentId))
                throw new ArgumentException($"Dupplicate instrument ID '{profile.InstrumentId}'.", nameof(profiles));
        }
    }

    /// <summary>
    /// Looks up a symbol without trimming it; returns false when no instrument matches.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="profile"></param>
    /// <returns></returns>
    public bool TryGetBySymbol(string symbol, out SimpleInstrumentProfile profile)
    {
        return _profiles.TryGetValue(symbol, out profile);
    }
}