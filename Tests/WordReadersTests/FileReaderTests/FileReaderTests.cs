using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using WordReaders.Readers;
using WordReaders.Settings;

namespace Tests.WordReadersTests.FileReaderTests;

[TestFixture]
public class FileReaderTests : BaseFileReaderTests
{
    protected override string FilesDirectoryName => "txtFiles";

    [Test]
    public void FileReader_InitializeWithNullSettings_ShouldThrowException()
    {
        var initAction = () => new FileReader(null);

        initAction.Should().Throw<ArgumentNullException>()
            .WithMessage("Reader settings cannot be null. (Parameter 'readerSettings')");
    }

    [Test]
    public void FileReader_InitializeWithNullPath_ShouldThrowException()
    {
        var initAction = () => new FileReader(new WordReaderSettings(null, Encoding.UTF8));

        initAction.Should().Throw<ArgumentNullException>()
            .WithMessage("File path cannot be null. (Parameter 'Path')");
    }

    [Test]
    public void FileReader_InitializeWithNullEncoding_ShouldThrowException()
    {
        var initAction = () => new FileReader(
            new WordReaderSettings($@"{GetFilesParentDir}/txt1_correct.txt", null));

        initAction.Should().Throw<ArgumentNullException>()
            .WithMessage("Encoding cannot be null. (Parameter 'Encoding')");
    }

    [Test]
    public void FileReader_InitializeWithNullPathAndEncoding_ShouldThrowException()
    {
        var initAction = () => new FileReader(
            new WordReaderSettings(null, null));

        initAction.Should().Throw<ArgumentNullException>()
            .WithMessage("File path cannot be null. (Parameter 'Path')");
    }

    [Test]
    public void FileReader_Initialize_ShouldThrowExceptionOnUnexistingTxt()
    {
        var initAction = () => new FileReader(
            new WordReaderSettings($@"{GetFilesParentDir}/unexisting.txt", Encoding.UTF8));

        initAction.Should().Throw<FileNotFoundException>();
    }

    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void FileReader_Read_ShouldReadNormalTxtCorrect()
    {
        var fileReader = new FileReader(new WordReaderSettings($@"{GetFilesParentDir}/txt1_correct.txt",
            Encoding.UTF8));

        Approvals.Verify(CreateWordsOutput(fileReader.Read().GetValueOrThrow()));
    }

    [Test]
    public void FileReader_Read_ShouldThrowExceptionOnWrongTxt()
    {
        var fileReader = new FileReader(new WordReaderSettings($@"{GetFilesParentDir}/txt_not_one_word_inline.txt",
            Encoding.UTF8));
        var readResult = fileReader.Read();

        readResult.IsSuccess.Should().BeFalse();
        readResult.Error.Should()
            .BeEquivalentTo("The file must contain no more than one word per line!");
    }

    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void FileReader_Read_ShouldReadTxtWithEmptyLinesCorrect()
    {
        var fileReader = new FileReader(new WordReaderSettings($@"{GetFilesParentDir}/txt_with_emptyline.txt",
            Encoding.UTF8));

        Approvals.Verify(CreateWordsOutput(fileReader.Read().GetValueOrThrow()));
    }

    [UseReporter(typeof(DiffReporter))]
    [Test]
    public void FileReader_Read_ShouldReadTxtWithWhiteSpacesCorrect()
    {
        var fileReader = new FileReader(new WordReaderSettings($@"{GetFilesParentDir}/txt_trim_spaces.txt",
            Encoding.UTF8));

        Approvals.Verify(CreateWordsOutput(fileReader.Read().GetValueOrThrow()));
    }
}