// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using NUnit.Framework;

namespace MudBlazor.UnitTests.Components;

[TestFixture]
public class DividerTests : BunitTest
{
    [Test]
    public void Defaults_ShouldExposeExpectedParameterValues()
    {
        var comp = Context.Render<MudDivider>();

        comp.Instance.Absolute.Should().BeFalse();
        comp.Instance.FlexItem.Should().BeFalse();
        comp.Instance.Light.Should().BeFalse();
        comp.Instance.Vertical.Should().BeFalse();
        comp.Instance.DividerType.Should().Be(DividerType.FullWidth);
    }

    [Test]
    public void Defaults_ShouldRenderHrWithBaseClasses()
    {
        var comp = Context.Render<MudDivider>();
        var hr = comp.Find("hr");

        hr.ClassList.Should().Contain("mud-divider");
        hr.ClassList.Should().Contain("mud-divider-fullwidth");
        hr.ClassList.Should().NotContain("mud-divider-absolute");
        hr.ClassList.Should().NotContain("mud-divider-flexitem");
        hr.ClassList.Should().NotContain("mud-divider-light");
        hr.ClassList.Should().NotContain("mud-divider-vertical");
    }

    [Test]
    public void Absolute_ShouldAddAbsoluteClass()
    {
        var comp = Context.Render<MudDivider>(parameters => parameters
            .Add(p => p.Absolute, true));

        var hr = comp.Find("hr");

        hr.ClassList.Should().Contain("mud-divider-absolute");
    }

    [Test]
    public void FlexItem_ShouldAddFlexItemClass()
    {
        var comp = Context.Render<MudDivider>(parameters => parameters
            .Add(p => p.FlexItem, true));

        var hr = comp.Find("hr");

        hr.ClassList.Should().Contain("mud-divider-flexitem");
    }

    [Test]
    public void Light_ShouldAddLightClass()
    {
        var comp = Context.Render<MudDivider>(parameters => parameters
            .Add(p => p.Light, true));

        var hr = comp.Find("hr");

        hr.ClassList.Should().Contain("mud-divider-light");
    }

    [Test]
    public void Vertical_ShouldAddVerticalClass()
    {
        var comp = Context.Render<MudDivider>(parameters => parameters
            .Add(p => p.Vertical, true));

        var hr = comp.Find("hr");

        hr.ClassList.Should().Contain("mud-divider-vertical");
    }

    [TestCase(DividerType.FullWidth, "mud-divider-fullwidth")]
    [TestCase(DividerType.Inset, "mud-divider-inset")]
    [TestCase(DividerType.Middle, "mud-divider-middle")]
    public void DividerType_ShouldRenderExpectedClass(DividerType dividerType, string expectedClass)
    {
        var comp = Context.Render<MudDivider>(parameters => parameters
            .Add(p => p.DividerType, dividerType));

        var hr = comp.Find("hr");

        hr.ClassList.Should().Contain(expectedClass);
    }

    [Test]
    public void VerticalWithFullWidth_ShouldNotAddFullWidthClass()
    {
        var comp = Context.Render<MudDivider>(parameters => parameters
            .Add(p => p.Vertical, true)
            .Add(p => p.DividerType, DividerType.FullWidth));

        var hr = comp.Find("hr");

        hr.ClassList.Should().Contain("mud-divider-vertical");
        hr.ClassList.Should().NotContain("mud-divider-fullwidth");
    }

    [Test]
    public void CustomClass_ShouldBeAppended()
    {
        var comp = Context.Render<MudDivider>(parameters => parameters
            .Add(p => p.Class, "my-divider-class"));

        var hr = comp.Find("hr");

        hr.ClassList.Should().Contain("my-divider-class");
    }

    [Test]
    public void UserAttributes_ShouldBeSplattedOnTheRootElement()
    {
        var comp = Context.Render<MudDivider>(parameters => parameters
            .AddUnmatched("data-test", "divider-value"));

        var hr = comp.Find("hr");

        hr.GetAttribute("data-test").Should().Be("divider-value");
    }
}
