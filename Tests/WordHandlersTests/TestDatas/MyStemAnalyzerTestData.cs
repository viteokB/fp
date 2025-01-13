using WordHandlers.MyStem.InfoClasses;

namespace Tests.WordHandlersTests.TestDatas;

public static class MyStemAnalyzerTestData
{
    // Существительные
    public static IEnumerable<TestCaseData> Nouns()
    {
        yield return new TestCaseData(new List<string> { "кот" },
            new List<WordInfo> { new("кот", PartOfSpeech.S, "кот") });
        yield return new TestCaseData(new List<string> { "дом" },
            new List<WordInfo> { new("дом", PartOfSpeech.S, "дом") });
    }

    //Глаголы
    public static IEnumerable<TestCaseData> Verbs()
    {
        yield return new TestCaseData(new List<string> { "бежать" },
            new List<WordInfo> { new("бежать", PartOfSpeech.V, "бежать") });
        yield return new TestCaseData(new List<string> { "спать" },
            new List<WordInfo> { new("спать", PartOfSpeech.V, "спать") });
    }

    // Прилагательные
    public static IEnumerable<TestCaseData> Adjectives()
    {
        yield return new TestCaseData(new List<string> { "счастливый" },
            new List<WordInfo> { new("счастливый", PartOfSpeech.A, "счастливый") });
        yield return new TestCaseData(new List<string> { "синий" },
            new List<WordInfo> { new("синий", PartOfSpeech.A, "синий") });
    }

    //Наречия
    public static IEnumerable<TestCaseData> Adverbs()
    {
        yield return new TestCaseData(new List<string> { "быстро" },
            new List<WordInfo> { new("быстро", PartOfSpeech.ADV, "быстро") });
        yield return new TestCaseData(new List<string> { "осторожно" },
            new List<WordInfo> { new("осторожно", PartOfSpeech.ADV, "осторожно") });
    }

    // Смешанные слова
    public static IEnumerable<TestCaseData> MixedWords()
    {
        yield return new TestCaseData(
            new List<string> { "кот", "бежать", "счастливый", "быстро" },
            new List<WordInfo>
            {
                new("кот", PartOfSpeech.S, "кот"),
                new("бежать", PartOfSpeech.V, "бежать"),
                new("счастливый", PartOfSpeech.A, "счастливый"),
                new("быстро", PartOfSpeech.ADV, "быстро")
            }
        );

        yield return new TestCaseData(
            new List<string> { "дом", "спать", "синий", "осторожно" },
            new List<WordInfo>
            {
                new("дом", PartOfSpeech.S, "дом"),
                new("спать", PartOfSpeech.V, "спать"),
                new("синий", PartOfSpeech.A, "синий"),
                new("осторожно", PartOfSpeech.ADV, "осторожно")
            }
        );
    }
}