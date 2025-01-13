using FluentAssertions;
using Tests.WordHandlersTests.TestDatas;
using WordHandlers.Handlers;

namespace Tests.WordHandlersTests;

[TestFixture]
public class boringHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        boringHandler = new BoringWordHandler();
    }

    [TearDown]
    public void TearDown()
    {
        boringHandler.Dispose();
    }

    private BoringWordHandler boringHandler;

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Nouns))]
    public void boringHandler_ApplyWordHandlerWithNouns_ReturnsNotEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Adjectives))]
    public void boringHandler_ApplyWordHandlerWithAdjectives_ReturnsNotEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Verbs))]
    public void boringHandler_ApplyWordHandlerWithVerbs_ReturnsNotEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Numerals))]
    public void boringHandler_ApplyWordHandlerWithNumerals_ReturnsNotEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Pronouns))]
    public void boringHandler_ApplyWordHandlerWithPronouns_ReturnsEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Adverbs))]
    public void boringHandler_ApplyWordHandlerWithAdverbs_ReturnsEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Conjunctions))]
    public void boringHandler_ApplyWordHandlerWithConjunctions_ReturnsEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Prepositions))]
    public void boringHandler_ApplyWordHandlerWithPrepositions_ReturnsEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }


    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Interjections))]
    public void boringHandler_ApplyWordHandlerWithInterjections_ReturnsEmptyLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.Particles))]
    public void boringHandler_ApplyWordHandlerWithParticles_ReturnsEmpryLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }

    [TestCaseSource(typeof(BoringWordTestData), nameof(BoringWordTestData.MixedWords))]
    public void boringHandler_ApplyWordHandlerWithMixedWords_ShouldReturnCorrectLemmas(List<string> inputWords,
        List<string> expectedWords)
    {
        var actualResult = boringHandler.ApplyWordHandler(inputWords);

        actualResult.Should().BeEquivalentTo(expectedWords);
    }
}