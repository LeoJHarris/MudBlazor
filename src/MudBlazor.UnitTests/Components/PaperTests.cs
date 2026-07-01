// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using NUnit.Framework;

namespace MudBlazor.UnitTests.Components;

[TestFixture]
public class PaperTests : BunitTest
{
    [Test]
    public void Defaults_ShouldExposeExpectedParameterValues()
    {
        var comp = Context.Render<MudPaper>();

        comp.Instance.Elevation.Should().Be(1);
        comp.Instance.Square.Should().BeFalse();
        comp.Instance.Outlined.Should().BeFalse();
        comp.Instance.Height.Should().BeNull();
        comp.Instance.Width.Should().BeNull();
        comp.Instance.MaxHeight.Should().BeNull();
        comp.Instance.MaxWidth.Should().BeNull();
        comp.Instance.MinHeight.Should().BeNull();
        comp.Instance.MinWidth.Should().BeNull();
    }

    [Test]
    public void Defaults_ShouldRenderWithBaseClassAndElevation()
    {
        var comp = Context.Render<MudPaper>();
        var div = comp.Find("div");

        div.ClassList.Should().Contain("mud-paper");
        div.ClassList.Should().Contain("mud-elevation-1");
        div.ClassList.Should().NotContain("mud-paper-outlined");
        div.ClassList.Should().NotContain("mud-paper-square");
    }

    [Test]
    public void ChildContent_ShouldRenderInsidePaper()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .AddChildContent("<span>Inner</span>"));

        comp.Find("div.mud-paper").InnerHtml.Should().Contain("<span>Inner</span>");
    }

    [TestCase(0, "mud-elevation-0")]
    [TestCase(1, "mud-elevation-1")]
    [TestCase(4, "mud-elevation-4")]
    [TestCase(24, "mud-elevation-24")]
    public void Elevation_ShouldRenderTheExpectedClass(int elevation, string expectedClass)
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.Elevation, elevation));

        var div = comp.Find("div.mud-paper");

        div.ClassList.Should().Contain(expectedClass);
    }

    [Test]
    public void Outlined_ShouldAddOutlinedClassAndRemoveElevation()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.Outlined, true));

        var div = comp.Find("div.mud-paper");

        div.ClassList.Should().Contain("mud-paper-outlined");
        div.ClassList.Should().NotContain("mud-elevation-1");
    }

    [Test]
    public void Square_ShouldAddSquareClass()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.Square, true));

        var div = comp.Find("div.mud-paper");

        div.ClassList.Should().Contain("mud-paper-square");
    }

    [Test]
    public void Height_ShouldApplyInlineStyle()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.Height, "200px"));

        var div = comp.Find("div.mud-paper");

        div.GetAttribute("style").Should().Contain("height:200px");
    }

    [Test]
    public void Width_ShouldApplyInlineStyle()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.Width, "300px"));

        var div = comp.Find("div.mud-paper");

        div.GetAttribute("style").Should().Contain("width:300px");
    }

    [Test]
    public void MaxHeight_ShouldApplyInlineStyle()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.MaxHeight, "500px"));

        var div = comp.Find("div.mud-paper");

        div.GetAttribute("style").Should().Contain("max-height:500px");
    }

    [Test]
    public void MaxWidth_ShouldApplyInlineStyle()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.MaxWidth, "400px"));

        var div = comp.Find("div.mud-paper");

        div.GetAttribute("style").Should().Contain("max-width:400px");
    }

    [Test]
    public void MinHeight_ShouldApplyInlineStyle()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.MinHeight, "100px"));

        var div = comp.Find("div.mud-paper");

        div.GetAttribute("style").Should().Contain("min-height:100px");
    }

    [Test]
    public void MinWidth_ShouldApplyInlineStyle()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.MinWidth, "150px"));

        var div = comp.Find("div.mud-paper");

        div.GetAttribute("style").Should().Contain("min-width:150px");
    }

    [Test]
    public void NullDimensions_ShouldNotApplyInlineStyles()
    {
        var comp = Context.Render<MudPaper>();
        var div = comp.Find("div.mud-paper");
        var style = div.GetAttribute("style");

        if (!string.IsNullOrEmpty(style))
        {
            style.Should().NotContain("height");
            style.Should().NotContain("width");
            style.Should().NotContain("max-height");
            style.Should().NotContain("max-width");
            style.Should().NotContain("min-height");
            style.Should().NotContain("min-width");
        }
    }

    [Test]
    public void CustomClass_ShouldBeAppended()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .Add(p => p.Class, "my-custom-class"));

        var div = comp.Find("div.mud-paper");

        div.ClassList.Should().Contain("my-custom-class");
    }

    [Test]
    public void UserAttributes_ShouldBeSplattedOnTheRootElement()
    {
        var comp = Context.Render<MudPaper>(parameters => parameters
            .AddUnmatched("data-test", "paper-value")
            .AddUnmatched("aria-label", "paper"));

        var div = comp.Find("div.mud-paper");

        div.GetAttribute("data-test").Should().Be("paper-value");
        div.GetAttribute("aria-label").Should().Be("paper");
    }
}
