using Oee.MarketAccess.Catalog;
using Oee.MarketAccess.Validation;

namespace Oee.MarketAccess.Tests.Validation;

public sealed class CommonTests
{
    private readonly Validator _validator;

    public CommonTests()
    {
        InMemoryInstrumentCatalog catalog = new();
        _validator = new(catalog);
    }

    [Fact]
    public void Null_Catalog_Must_Be_Rejected()
    {
        Assert.Throws<ArgumentNullException>(() => new Validator(null!));
    }

    [Fact]
    public void Null_Message_Must_Be_Rejected()
    {
        Assert.Throws<ArgumentNullException>(() => _validator.Validate(null!));
    }
}