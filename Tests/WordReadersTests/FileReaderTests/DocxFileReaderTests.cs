using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using WordReaders.Readers;
using WordReaders.Settings;

namespace Tests.WordReadersTests.FileReaderTests;

[TestFixture]
public class DocxFileReaderTests : BaseFileReaderTests
{
    protected override string FilesDirectoryName => "docxFiles";

    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void DocxFileReader_Read_ShouldReadNormalDocxCorrect()
    {
        var docxReader =
            new DocxFileReader(new WordReaderSettings($@"{GetFilesParentDir}/docx1_correct.docx", Encoding.UTF8));

        Approvals.Verify(CreateWordsOutput(docxReader.Read().GetValueOrThrow()));
    }

    [Test]
    public void DocxFileReader_Read_ShouldThrowExceptionOnWrongDocx()
    {
        var docxReader =
            new DocxFileReader(new WordReaderSettings($@"{GetFilesParentDir}/docx_not_one_word_inline.docx",
                Encoding.UTF8));
        var readResult= docxReader.Read();

        readResult.IsSuccess.Should().BeFalse();
        readResult.Error.Should()
            .BeEquivalentTo("The docx file must contain no more than one word per line!");
    }

    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void DocxFileReader_Read_ShouldReadDocxWithEmptyLinesCorrect()
    {
        var docxReader =
            new DocxFileReader(new WordReaderSettings($@"{GetFilesParentDir}/docx_with_emptyline.docx", Encoding.UTF8));

        Approvals.Verify(CreateWordsOutput(docxReader.Read().GetValueOrThrow()));
    }

    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void DocxFileReader_Read_ShouldReadDocxWithWhiteSpacesCorrect()
    {
        var docxReader =
            new DocxFileReader(new WordReaderSettings($@"{GetFilesParentDir}/docx_trim_spaces.docx", Encoding.UTF8));

        Approvals.Verify(CreateWordsOutput(docxReader.Read().GetValueOrThrow()));
    }
}