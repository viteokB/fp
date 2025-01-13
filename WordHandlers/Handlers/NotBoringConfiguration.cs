using WordHandlers.MyStem.InfoClasses;

namespace WordHandlers.Handlers;

public static class NotBoringConfiguration
{
    public static HashSet<PartOfSpeech> NotBoringPartOfSpeeches =>
    [
        PartOfSpeech.A,
        PartOfSpeech.V,
        PartOfSpeech.S
    ];
}