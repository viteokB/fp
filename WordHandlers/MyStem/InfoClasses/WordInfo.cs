namespace WordHandlers.MyStem.InfoClasses;

public class WordInfo
{
    public WordInfo(string text, PartOfSpeech partOfSpeech, string lemma)
    {
        Text = text;
        PartOfSpeech = partOfSpeech;
        Lemma = lemma;
    }

    public string Text { get; set; }

    public PartOfSpeech PartOfSpeech { get; set; }

    public string Lemma { get; set; }
}