namespace Oee.MarketAccess.Catalog;

public static class SyntheticInstrumentSeed
{
    public static SimpleInstrumentProfile[] CreateProfiles() => [
        new(1, "AAPL", true, 0.01m, 100, 1000, 30000, 36000),
        new(2, "NVDA", false, 0.01m, 100, 1000, 10000, 20000)
    ];
}