// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using NUnit.Framework;

namespace MudBlazor.UnitTests.Components;

[TestFixture]
public class SimpleTableTests : BunitTest
{
    [Test]
    public void Defaults_ShouldExposeExpectedParameterValues()
    {
        var comp = Context.Render<MudSimpleTable>();

        comp.Instance.Elevation.Should().Be(1);
        comp.Instance.Hover.Should().BeFalse();
        comp.Instance.Square.Should().BeFalse();
        comp.Instance.Dense.Should().BeFalse();
        comp.Instance.Outlined.Should().BeFalse();
        comp.Instance.Bordered.Should().BeFalse();
        comp.Instance.Striped.Should().BeFalse();
        comp.Instance.FixedHeader.Should().BeFalse();
    }

    [Test]
    public void Defaults_ShouldRenderWithBaseClassAndElevation()
    {
        var comp = Context.Render<MudSimpleTable>();
        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("mud-table");
        div.ClassList.Should().Contain("mud-simple-table");
        div.ClassList.Should().Contain("mud-elevation-1");
    }

    [Test]
    public void Defaults_ShouldRenderTableContainerAndTableElement()
    {
        var comp = Context.Render<MudSimpleTable>();

        comp.Find("div.mud-table-container").Should().NotBeNull();
        comp.Find("table").Should().NotBeNull();
    }

    [Test]
    public void Dense_ShouldAddDenseClass()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.Dense, true));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("mud-table-dense");
    }

    [Test]
    public void Hover_ShouldAddHoverClass()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.Hover, true));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("mud-table-hover");
    }

    [Test]
    public void Bordered_ShouldAddBorderedClass()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.Bordered, true));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("mud-table-bordered");
    }

    [Test]
    public void Outlined_ShouldAddOutlinedClassAndRemoveElevation()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.Outlined, true));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("mud-table-outlined");
        div.ClassList.Should().NotContain("mud-elevation-1");
    }

    [Test]
    public void Striped_ShouldAddStripedClass()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.Striped, true));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("mud-table-striped");
    }

    [Test]
    public void Square_ShouldAddSquareClass()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.Square, true));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("mud-table-square");
    }

    [Test]
    public void FixedHeader_ShouldAddStickyHeaderClass()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.FixedHeader, true));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("mud-table-sticky-header");
    }

    [TestCase(0, "mud-elevation-0")]
    [TestCase(1, "mud-elevation-1")]
    [TestCase(4, "mud-elevation-4")]
    [TestCase(24, "mud-elevation-24")]
    public void Elevation_ShouldRenderExpectedClass(int elevation, string expectedClass)
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.Elevation, elevation));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain(expectedClass);
    }

    [Test]
    public void ChildContent_ShouldRenderInsideTable()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .AddChildContent("<tbody><tr><td>Cell</td></tr></tbody>"));

        comp.Find("table").InnerHtml.Should().Contain("Cell");
    }

    [Test]
    public void CustomClass_ShouldBeAppended()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .Add(p => p.Class, "my-table-class"));

        var div = comp.Find("div.mud-simple-table");

        div.ClassList.Should().Contain("my-table-class");
    }

    [Test]
    public void UserAttributes_ShouldBeSplattedOnTheRootElement()
    {
        var comp = Context.Render<MudSimpleTable>(parameters => parameters
            .AddUnmatched("data-test", "table-value"));

        var div = comp.Find("div.mud-simple-table");

        div.GetAttribute("data-test").Should().Be("table-value");
    }
}
