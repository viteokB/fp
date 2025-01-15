using FluentAssertions;
using Tests.WordHandlersTests.TestDatas;
using WordHandlers.Handlers;

namespace Tests.WordHandlersTests;

[TestFixture]
public class LowercaseWordHandlerTests
{
    [OneTimeSetUp]
    protected void SetUp()
    {
        lowercaseWordHandler = new LowercaseWordHandler();
    }

    private LowercaseWordHandler lowercaseWordHandler;

    [Test]
    public void LowercaseWordHandler_ApplyHandler_ShouldThrowExceptionOnNullArgument()
    {
        var actual = lowercaseWordHandler.ApplyWordHandler(null);

        actual.IsSuccess.Should().BeFalse();
        actual.Error.Should().BeEquivalentTo("Words argument cannot be null.");
    }

    [Test]
    public void LowercaseWordHandler_ApplyHandlerToEmptyEnumerable_ShouldReturnEmptyEnumerable()
    {
        var actual = lowercaseWordHandler.ApplyWordHandler([]);

        actual.IsSuccess.Should().BeTrue();
        actual.GetValueOrThrow().Should().BeEquivalentTo(Enumerable.Empty<string>());
    }

    [TestCaseSource(typeof(LowercaseTestData), nameof(LowercaseTestData.OneWordCases))]
    public void LowercaseWordHandler_ApplyHandler_ShouldLowerOneWord(List<string> given, List<string> expected)
    {
        var actual = lowercaseWordHandler.ApplyWordHandler(given).GetValueOrThrow();

        actual.Should().BeEquivalentTo(expected);
    }

    [TestCaseSource(typeof(LowercaseTestData), nameof(LowercaseTestData.FewWordsCases))]
    public void LowercaseWordHandler_ApplyHandler_ShouldLowerFewWords(List<string> given, List<string> expected)
    {
        var actual = lowercaseWordHandler.ApplyWordHandler(given).GetValueOrThrow();

        actual.Should().BeEquivalentTo(expected);
    }
}