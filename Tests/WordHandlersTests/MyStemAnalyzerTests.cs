using System.Diagnostics;
using System.Reflection;
using FluentAssertions;
using Tests.WordHandlersTests.TestDatas;
using WordHandlers.MyStem;
using WordHandlers.MyStem.InfoClasses;

namespace Tests.WordHandlersTests;

[TestFixture]
public class MyStemAnalyzerTests
{
    [SetUp]
    public void SetUp()
    {
        analyzer = new MyStemAnalyzer();
    }

    [TearDown]
    public void TearDown()
    {
        analyzer.Dispose();
    }

    private MyStemAnalyzer analyzer;

    [Test]
    public void MyStemAnalyzer_AnalyzeWord_IsNotNull()
    {
        var word = new List<string> { "слово" };
        var wordInfo = analyzer.AnalyzeWords(word);
        wordInfo.Should().NotBeNull();
    }

    [TestCaseSource(typeof(MyStemAnalyzerTestData), nameof(MyStemAnalyzerTestData.Nouns))]
    public void MyStemAnalyzer_AnalyzeWord_ShouldAnalyzeNouns(List<string> given, List<WordInfo> expectedInfo)
    {
        var wordInfo = analyzer.AnalyzeWords(given);
        wordInfo.Should().BeEquivalentTo(expectedInfo);
    }

    [TestCaseSource(typeof(MyStemAnalyzerTestData), nameof(MyStemAnalyzerTestData.Verbs))]
    public void MyStemAnalyzer_AnalyzeWord_ShouldAnalyzeVerbs(List<string> given, List<WordInfo> expectedInfo)
    {
        var wordInfo = analyzer.AnalyzeWords(given);
        wordInfo.Should().BeEquivalentTo(expectedInfo);
    }

    [TestCaseSource(typeof(MyStemAnalyzerTestData), nameof(MyStemAnalyzerTestData.Adjectives))]
    public void MyStemAnalyzer_AnalyzeWord_ShouldAnalyzeAdjectives(List<string> given, List<WordInfo> expectedInfo)
    {
        var wordInfo = analyzer.AnalyzeWords(given);
        wordInfo.Should().BeEquivalentTo(expectedInfo);
    }

    [TestCaseSource(typeof(MyStemAnalyzerTestData), nameof(MyStemAnalyzerTestData.Adverbs))]
    public void MyStemAnalyzer_AnalyzeWord_ShouldAnalyzeAdverbs(List<string> given, List<WordInfo> expectedInfo)
    {
        var wordInfo = analyzer.AnalyzeWords(given);
        wordInfo.Should().BeEquivalentTo(expectedInfo);
    }

    [TestCaseSource(typeof(MyStemAnalyzerTestData), nameof(MyStemAnalyzerTestData.MixedWords))]
    public void MyStemAnalyzer_AnalyzeWord_ShouldAnalyzeMixedWords(List<string> given, List<WordInfo> expectedInfo)
    {
        var wordInfo = analyzer.AnalyzeWords(given);
        wordInfo.Should().BeEquivalentTo(expectedInfo);
    }

    [TestCaseSource(typeof(MyStemAnalyzerTestData), nameof(MyStemAnalyzerTestData.MixedWords))]
    public void MyStemAnalyzer_AnalyzeWord_ShouldAnalyzeEveryTimeAfterFirst(List<string> given,
        List<WordInfo> expectedInfo)
    {
        var wordInfo = analyzer.AnalyzeWords(given);
        wordInfo.Should().BeEquivalentTo(expectedInfo);

        wordInfo = analyzer.AnalyzeWords(given);
        wordInfo.Should().BeEquivalentTo(expectedInfo);
    }

    [Test]
    public void MyStemAnalyzer_AnalyzeWord_ShouldDisposeProcess()
    {
        var processField = typeof(MyStemAnalyzer).GetField("process", BindingFlags.Static | BindingFlags.NonPublic);

        analyzer.Dispose();

        var process = processField?.GetValue(null) as Process;
        process?.HasExited.Should().BeTrue("Process should be disposed");
    }
}