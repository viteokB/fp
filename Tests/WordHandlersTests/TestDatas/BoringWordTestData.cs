namespace Tests.WordHandlersTests.TestDatas;

public static class BoringWordTestData
{
    // Существительные 1
    public static IEnumerable<TestCaseData> Nouns()
    {
        yield return new TestCaseData(new List<string> { "кот" }, new List<string> { "кот" });
        yield return new TestCaseData(new List<string> { "дом" }, new List<string> { "дом" });
        yield return new TestCaseData(new List<string> { "книга" }, new List<string> { "книга" });
    }

    // Прилагательные 1
    public static IEnumerable<TestCaseData> Adjectives()
    {
        yield return new TestCaseData(new List<string> { "красный" }, new List<string> { "красный" });
        yield return new TestCaseData(new List<string> { "большой" }, new List<string> { "большой" });
        yield return new TestCaseData(new List<string> { "зелёный" }, new List<string> { "зеленый" });
    }

    // Глаголы 1
    public static IEnumerable<TestCaseData> Verbs()
    {
        yield return new TestCaseData(new List<string> { "бежать" }, new List<string> { "бежать" });
        yield return new TestCaseData(new List<string> { "думать" }, new List<string> { "думать" });
        yield return new TestCaseData(new List<string> { "читать" }, new List<string> { "читать" });
    }

    // Числительные 1
    public static IEnumerable<TestCaseData> Numerals()
    {
        yield return new TestCaseData(new List<string> { "один" }, new List<string>());
        yield return new TestCaseData(new List<string> { "два" }, new List<string>());
        yield return new TestCaseData(new List<string> { "пять" }, new List<string>());
    }

    // Местоимения
    public static IEnumerable<TestCaseData> Pronouns()
    {
        yield return new TestCaseData(new List<string> { "я" }, new List<string>());
        yield return new TestCaseData(new List<string> { "ты" }, new List<string>());
        yield return new TestCaseData(new List<string> { "он" }, new List<string>());
        yield return new TestCaseData(new List<string> { "мой" }, new List<string>());
    }


    // Наречия
    public static IEnumerable<TestCaseData> Adverbs()
    {
        yield return new TestCaseData(new List<string> { "быстро" }, new List<string>());
        yield return new TestCaseData(new List<string> { "медленно" }, new List<string>());
        yield return new TestCaseData(new List<string> { "здесь" }, new List<string>());
    }

    // Союзы
    public static IEnumerable<TestCaseData> Conjunctions()
    {
        yield return new TestCaseData(new List<string> { "и" }, new List<string>());
        yield return new TestCaseData(new List<string> { "но" }, new List<string>());
        yield return new TestCaseData(new List<string> { "что" }, new List<string>());
    }

    // Предлоги
    public static IEnumerable<TestCaseData> Prepositions()
    {
        yield return new TestCaseData(new List<string> { "в" }, new List<string>());
        yield return new TestCaseData(new List<string> { "на" }, new List<string>());
        yield return new TestCaseData(new List<string> { "к" }, new List<string>());
    }

    // Междометия
    public static IEnumerable<TestCaseData> Interjections()
    {
        yield return new TestCaseData(new List<string> { "ой" }, new List<string>());
        yield return new TestCaseData(new List<string> { "ах" }, new List<string>());
        yield return new TestCaseData(new List<string> { "ух" }, new List<string>());
    }

    // Частицы
    public static IEnumerable<TestCaseData> Particles()
    {
        yield return new TestCaseData(new List<string> { "же" }, new List<string>());
        yield return new TestCaseData(new List<string> { "ли" }, new List<string>());
        yield return new TestCaseData(new List<string> { "не" }, new List<string>());
    }


    public static IEnumerable<TestCaseData> MixedWords()
    {
        yield return new TestCaseData(
            new List<string> { "коты", "бегали", "быстро", "по", "зеленой", "траве" },
            new List<string> { "кот", "бегать", "зеленый", "трава" });
        yield return new TestCaseData(
            new List<string> { "красные", "яблоки", "лежали", "на", "столе" },
            new List<string> { "красный", "яблоко", "лежать", "стол" });
        yield return new TestCaseData(
            new List<string> { "два", "кота", "сидели", "на", "окне", "и", "смотрели", "вверх" },
            new List<string> { "кот", "сидеть", "окно", "смотреть" });
    }
}