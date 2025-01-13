namespace Tests.WordHandlersTests.TestDatas;

public static class LowercaseTestData
{
    public static IEnumerable<TestCaseData> OneWordCases()
    {
        yield return new TestCaseData(new List<string> { "заниженный" },
            new List<string> { "заниженный" });
        yield return new TestCaseData(new List<string> { "однаБуква" },
            new List<string> { "однабуква" });
        yield return new TestCaseData(new List<string> { "неСкОлькОбукОв" },
            new List<string> { "несколькобуков" });
    }

    public static IEnumerable<TestCaseData> FewWordsCases()
    {
        yield return new TestCaseData(new List<string> { "Эх", "НГ", "скорей", "бы" },
            new List<string> { "эх", "нг", "скорей", "бы" });
        yield return new TestCaseData(new List<string> { "ОТДОХНУ", "ЗНАТНО", "ПОСЛЕ", "УЧЕБЫ" },
            new List<string> { "отдохну", "знатно", "после", "учебы" });
        yield return new TestCaseData(new List<string> { "ДожитЬ", "Бы", "До", "Следующей", "Недели" },
            new List<string> { "дожить", "бы", "до", "следующей", "недели" });
    }
}