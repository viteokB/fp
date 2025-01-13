using System.Drawing;
using FluentAssertions;
using TagCloud;
using TestHelpers.TagCloudLayouterTests.Helpers;
using TestHelpers.TagCloudLayouterTests.Helpers.RectangleOnlyVisualiser;
using Tests.TagCloudLayouterTests.TestHelpers;

namespace Tests.TagCloudLayouterTests;

public abstract class BaseTagCloudTests
{
    protected const int WIDTH = 1000;

    protected const int HEIGHT = 1000;

    protected Point _center;

    protected Size _errorImageSize;

    protected TagCloudLayouter _layouter;

    protected TagCloudImageGenerator _tagCloudImageGenerator;

    protected IRectangleVisualiser _visualiser;

    [SetUp]
    public abstract void Setup();

    [TearDown]
    public abstract void Teardown();

    [Test]
    public void TagCloudLayouter_WhenCreated_RectanglesShoudBeEmpty()
    {
        _layouter.Rectangles.Should().BeEmpty();
    }

    [Test]
    public virtual void TagCloudLayouter_WhenCreated_FirstPointEqualsCenter()
    {
        var firstPoint = _layouter.PointGenerator.GeneratePoints().First();

        firstPoint.Should().BeEquivalentTo(_center);
    }

    [Test]
    public void TagCloudLayouter_WhenAddFirstRectangle_ContainOneAndSameRectangle()
    {
        var rectangle = _layouter.PutNextRectangle(new Size(20, 10));

        _layouter.Rectangles
            .Select(r => r).Should().HaveCount(1).And.Contain(rectangle);
    }

    [Test]
    public void TagCloudLayouter_WhenAddRectangle_ReturnsRectangleWithSameSize()
    {
        var givenSize = new Size(20, 20);

        var rectangle = _layouter.PutNextRectangle(givenSize);

        rectangle.Size.Should().BeEquivalentTo(givenSize);
    }

    [TestCase(0, 0)]
    [TestCase(-2, 2)]
    [TestCase(2, -2)]
    [TestCase(-2, -2)]
    public void TagCloudLayouter_WhenWrongSize_ThrowArgumentException(int width, int height)
    {
        Assert.Throws<ArgumentException>(() => _layouter.PutNextRectangle(new Size(width, height)),
            "Not valid size should be positive");
    }

    [Test]
    public void TagCloudLayouter_WhenAddFew_ShouldHaveSameCount()
    {
        var sizesList = GenerateSizes(5);
        AddRectanglesToLayouter(sizesList);

        _layouter.Rectangles.Should().HaveCount(5);
    }

    [TestCase(10, 20, 30)]
    public void TagCloudLayouter_ShouldIncreaseRectangleCountCorrectly(int countBefore, int add, int countAfter)
    {
        var countBeforeSizes = GenerateSizes(countBefore);
        AddRectanglesToLayouter(countBeforeSizes);

        var addSizes = GenerateSizes(add);
        AddRectanglesToLayouter(addSizes);

        _layouter.Rectangles.Should().HaveCount(countAfter);
    }

    [TestCase(5)]
    [TestCase(60)]
    public void TagCloudLayouter_WhenAddFew_RectangleNotIntersectsWithOtherRectangles(int count)
    {
        var listSizes = GenerateSizes(count);
        AddRectanglesToLayouter(listSizes);

        foreach (var rectangle in _layouter.Rectangles)
            _layouter.Rectangles
                .Any(r => r.IntersectsWith(rectangle) && rectangle != r)
                .Should().BeFalse();
    }

    protected List<Size> GenerateSizes(int count)
    {
        return SizeBuilder.Configure()
            .SetCount(count)
            .SetWidth(60, 80)
            .SetHeight(60, 80)
            .Generate()
            .ToList();
    }

    protected void AddRectanglesToLayouter(List<Size> sizes)
    {
        sizes.ForEach(size => _layouter.PutNextRectangle(size));
    }
}