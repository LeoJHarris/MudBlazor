// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using NUnit.Framework;

namespace MudBlazor.UnitTests.Components;

[TestFixture]
public class GridTests : BunitTest
{
    [Test]
    public void MudGrid_Defaults_ShouldExposeExpectedParameterValues()
    {
        var comp = Context.Render<MudGrid>();

        comp.Instance.Spacing.Should().Be(6);
        comp.Instance.Justify.Should().Be(Justify.FlexStart);
    }

    [Test]
    public void MudGrid_Defaults_ShouldRenderWithBaseClasses()
    {
        var comp = Context.Render<MudGrid>();
        var div = comp.Find("div.mud-grid");

        div.ClassList.Should().Contain("mud-grid");
        div.ClassList.Should().Contain("mud-grid-spacing-xs-6");
        div.ClassList.Should().Contain("justify-start");
    }

    [TestCase(0, "mud-grid-spacing-xs-0")]
    [TestCase(2, "mud-grid-spacing-xs-2")]
    [TestCase(6, "mud-grid-spacing-xs-6")]
    [TestCase(10, "mud-grid-spacing-xs-10")]
    public void MudGrid_Spacing_ShouldRenderExpectedClass(int spacing, string expectedClass)
    {
        var comp = Context.Render<MudGrid>(parameters => parameters
            .Add(p => p.Spacing, spacing));

        var div = comp.Find("div.mud-grid");

        div.ClassList.Should().Contain(expectedClass);
    }

    [TestCase(Justify.FlexStart, "justify-start")]
    [TestCase(Justify.Center, "justify-center")]
    [TestCase(Justify.FlexEnd, "justify-end")]
    [TestCase(Justify.SpaceBetween, "justify-space-between")]
    [TestCase(Justify.SpaceAround, "justify-space-around")]
    [TestCase(Justify.SpaceEvenly, "justify-space-evenly")]
    public void MudGrid_Justify_ShouldRenderExpectedClass(Justify justify, string expectedClass)
    {
        var comp = Context.Render<MudGrid>(parameters => parameters
            .Add(p => p.Justify, justify));

        var div = comp.Find("div.mud-grid");

        div.ClassList.Should().Contain(expectedClass);
    }

    [Test]
    public void MudGrid_ChildContent_ShouldRenderInside()
    {
        var comp = Context.Render<MudGrid>(parameters => parameters
            .AddChildContent("<span>grid-child</span>"));

        comp.Find("div.mud-grid").InnerHtml.Should().Contain("<span>grid-child</span>");
    }

    [Test]
    public void MudGrid_CustomClass_ShouldBeAppended()
    {
        var comp = Context.Render<MudGrid>(parameters => parameters
            .Add(p => p.Class, "my-grid-class"));

        var div = comp.Find("div.mud-grid");

        div.ClassList.Should().Contain("my-grid-class");
    }

    [Test]
    public void MudGrid_UserAttributes_ShouldBeSplattedOnTheRootElement()
    {
        var comp = Context.Render<MudGrid>(parameters => parameters
            .AddUnmatched("data-test", "grid-value"));

        var div = comp.Find("div.mud-grid");

        div.GetAttribute("data-test").Should().Be("grid-value");
    }

    [Test]
    public void MudItem_Defaults_ShouldRenderWithBaseClass()
    {
        var comp = Context.Render<MudItem>();
        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("mud-grid-item");
        div.ClassList.Should().NotContain("mud-grid-item-xs-");
    }

    [Test]
    public void MudItem_Xs_ShouldRenderBreakpointClass()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.xs, 6));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("mud-grid-item-xs-6");
    }

    [Test]
    public void MudItem_Sm_ShouldRenderBreakpointClass()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.sm, 4));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("mud-grid-item-sm-4");
    }

    [Test]
    public void MudItem_Md_ShouldRenderBreakpointClass()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.md, 3));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("mud-grid-item-md-3");
    }

    [Test]
    public void MudItem_Lg_ShouldRenderBreakpointClass()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.lg, 8));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("mud-grid-item-lg-8");
    }

    [Test]
    public void MudItem_Xl_ShouldRenderBreakpointClass()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.xl, 2));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("mud-grid-item-xl-2");
    }

    [Test]
    public void MudItem_Xxl_ShouldRenderBreakpointClass()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.xxl, 10));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("mud-grid-item-xxl-10");
    }

    [Test]
    public void MudItem_MultipleBreakpoints_ShouldRenderAllClasses()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.xs, 12)
            .Add(p => p.sm, 6)
            .Add(p => p.md, 4));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("mud-grid-item-xs-12");
        div.ClassList.Should().Contain("mud-grid-item-sm-6");
        div.ClassList.Should().Contain("mud-grid-item-md-4");
    }

    [Test]
    public void MudItem_ZeroBreakpoint_ShouldNotRenderClass()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.xs, 0));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().NotContain("mud-grid-item-xs-0");
    }

    [Test]
    public void MudItem_ChildContent_ShouldRenderInside()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .AddChildContent("<span>item-child</span>"));

        comp.Find("div.mud-grid-item").InnerHtml.Should().Contain("<span>item-child</span>");
    }

    [Test]
    public void MudItem_CustomClass_ShouldBeAppended()
    {
        var comp = Context.Render<MudItem>(parameters => parameters
            .Add(p => p.Class, "my-item-class"));

        var div = comp.Find("div.mud-grid-item");

        div.ClassList.Should().Contain("my-item-class");
    }

    [Test]
    public void MudFlexBreak_ShouldRenderWithBaseClass()
    {
        var comp = Context.Render<MudFlexBreak>();
        var div = comp.Find("div.mud-flex-break");

        div.ClassList.Should().Contain("mud-flex-break");
        div.GetAttribute("aria-hidden").Should().Be("true");
    }

    [Test]
    public void MudFlexBreak_CustomClass_ShouldBeAppended()
    {
        var comp = Context.Render<MudFlexBreak>(parameters => parameters
            .Add(p => p.Class, "custom-break"));

        var div = comp.Find("div.mud-flex-break");

        div.ClassList.Should().Contain("custom-break");
    }
}
