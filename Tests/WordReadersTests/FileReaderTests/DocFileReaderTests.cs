using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using WordReaders.Readers;
using WordReaders.Settings;

namespace Tests.WordReadersTests.FileReaderTests;

[TestFixture]
public class DocFileReaderTests : BaseFileReaderTests
{
    protected override string FilesDirectoryName => "docFiles";

    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void DocFileReader_Read_ShouldReadNormalDocCorrect()
    {
        var docReader = new DocFileReader(new WordReaderSettings(@$"{GetFilesParentDir}/doc1_correct.doc",
            Encoding.UTF8));

        Approvals.Verify(CreateWordsOutput(docReader.Read().GetValueOrThrow()));
    }


    [Test]
    public void DocFileReader_Read_ShouldThrowExceptionOnWrongDoc()
    {
        var docReader = new DocFileReader(new WordReaderSettings(@$"{GetFilesParentDir}/doc_not_one_word_inline.doc",
            Encoding.UTF8));
        var readResult = docReader.Read();

        readResult.IsSuccess.Should().BeFalse();
        readResult.Error.Should()
            .BeEquivalentTo("The doc file must contain no more than one word per line!");
    }

    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void DocFileReader_Read_ShouldReadDocWithEmptyLinesCorrect()
    {
        var docReader = new DocFileReader(new WordReaderSettings($@"{GetFilesParentDir}/doc_with_emptyline.doc",
            Encoding.UTF8));
        Approvals.Verify(CreateWordsOutput(docReader.Read().GetValueOrThrow()));
    }


    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void DocFileReader_Read_ShouldReadDocWithWhiteSpacesCorrect()
    {
        var docReader = new DocFileReader(new WordReaderSettings($@"{GetFilesParentDir}/doc_trim_spaces.doc",
            Encoding.UTF8));
        Approvals.Verify(CreateWordsOutput(docReader.Read().GetValueOrThrow()));
    }
}