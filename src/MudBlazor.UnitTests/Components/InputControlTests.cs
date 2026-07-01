// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using NUnit.Framework;

namespace MudBlazor.UnitTests.Components;

[TestFixture]
public class InputControlTests : BunitTest
{
    [Test]
    public void Defaults_ShouldExposeExpectedParameterValues()
    {
        var comp = Context.Render<MudInputControl>();

        comp.Instance.Margin.Should().Be(Margin.None);
        comp.Instance.Required.Should().BeFalse();
        comp.Instance.Error.Should().BeFalse();
        comp.Instance.ErrorText.Should().BeNull();
        comp.Instance.ErrorId.Should().BeNull();
        comp.Instance.HelperText.Should().BeNull();
        comp.Instance.HelperId.Should().BeNull();
        comp.Instance.HelperTextOnFocus.Should().BeFalse();
        comp.Instance.CounterText.Should().BeNull();
        comp.Instance.FullWidth.Should().BeFalse();
        comp.Instance.Label.Should().BeNull();
        comp.Instance.Variant.Should().Be(Variant.Text);
        comp.Instance.Disabled.Should().BeFalse();
    }

    [Test]
    public void Defaults_ShouldRenderWithBaseClass()
    {
        var comp = Context.Render<MudInputControl>();
        var div = comp.Find("div.mud-input-control");

        div.ClassList.Should().Contain("mud-input-control");
        div.ClassList.Should().NotContain("mud-input-required");
        div.ClassList.Should().NotContain("mud-input-control-full-width");
        div.ClassList.Should().NotContain("mud-input-error");
    }

    [Test]
    public void Required_ShouldAddRequiredClass()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Required, true));

        var div = comp.Find("div.mud-input-control");

        div.ClassList.Should().Contain("mud-input-required");
    }

    [Test]
    public void FullWidth_ShouldAddFullWidthClass()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.FullWidth, true));

        var div = comp.Find("div.mud-input-control");

        div.ClassList.Should().Contain("mud-input-control-full-width");
    }

    [Test]
    public void Error_ShouldAddErrorClass()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Error, true));

        var div = comp.Find("div.mud-input-control");

        div.ClassList.Should().Contain("mud-input-error");
    }

    [TestCase(Margin.None)]
    [TestCase(Margin.Dense)]
    [TestCase(Margin.Normal)]
    public void Margin_ShouldRenderExpectedClass(Margin margin)
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Margin, margin));

        var div = comp.Find("div.mud-input-control");

        if (margin == Margin.None)
        {
            div.ClassList.Should().NotContain("mud-input-control-margin-dense");
            div.ClassList.Should().NotContain("mud-input-control-margin-normal");
        }
        else
        {
            div.ClassList.Should().Contain($"mud-input-control-margin-{margin.ToStringFast(true)}");
        }
    }

    [Test]
    public void Label_ShouldRenderInputLabel()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Label, "Test Label"));

        comp.Markup.Should().Contain("Test Label");
        comp.Find("div.mud-input-control").ClassList.Should().Contain("mud-input-text-with-label");
    }

    [Test]
    public void NoLabel_ShouldNotRenderInputLabel()
    {
        var comp = Context.Render<MudInputControl>();

        comp.FindAll("label").Should().BeEmpty();
        comp.Find("div.mud-input-control").ClassList.Should().NotContain("mud-input-text-with-label");
    }

    [Test]
    public void ErrorText_ShouldRenderWhenErrorIsTrue()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Error, true)
            .Add(p => p.ErrorText, "Something went wrong")
            .Add(p => p.ErrorId, "error-1"));

        comp.Markup.Should().Contain("Something went wrong");
        var errorDiv = comp.Find("#error-1");
        errorDiv.TextContent.Should().Contain("Something went wrong");
    }

    [Test]
    public void HelperText_ShouldRenderWhenNotInError()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.HelperText, "Enter your name")
            .Add(p => p.HelperId, "helper-1"));

        comp.Markup.Should().Contain("Enter your name");
        var helperDiv = comp.Find("#helper-1");
        helperDiv.TextContent.Should().Contain("Enter your name");
    }

    [Test]
    public void ErrorText_ShouldTakePrecedenceOverHelperText()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Error, true)
            .Add(p => p.ErrorText, "Error message")
            .Add(p => p.HelperText, "Helper message"));

        comp.Markup.Should().Contain("Error message");
        comp.Markup.Should().NotContain("Helper message");
    }

    [Test]
    public void CounterText_ShouldRender()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.CounterText, "5/100"));

        comp.Markup.Should().Contain("5/100");
    }

    [Test]
    public void HelperTextOnFocus_ShouldAddOnFocusClass()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.HelperText, "Focus helper")
            .Add(p => p.HelperTextOnFocus, true));

        comp.Markup.Should().Contain("mud-input-helper-onfocus");
    }

    [TestCase(Variant.Text)]
    [TestCase(Variant.Filled)]
    [TestCase(Variant.Outlined)]
    public void Variant_WithLabel_ShouldRenderExpectedClass(Variant variant)
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Variant, variant)
            .Add(p => p.Label, "Label"));

        var div = comp.Find("div.mud-input-control");

        div.ClassList.Should().Contain($"mud-input-{variant.ToStringFast(true)}-with-label");
    }

    [Test]
    public void HelperContainer_FilledVariant_ShouldHavePx1Class()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Variant, Variant.Filled)
            .Add(p => p.HelperText, "help"));

        var container = comp.Find(".mud-input-control-helper-container");

        container.ClassList.Should().Contain("px-1");
    }

    [Test]
    public void HelperContainer_OutlinedVariant_ShouldHavePx2Class()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Variant, Variant.Outlined)
            .Add(p => p.HelperText, "help"));

        var container = comp.Find(".mud-input-control-helper-container");

        container.ClassList.Should().Contain("px-2");
    }

    [Test]
    public void CustomClass_ShouldBeAppended()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .Add(p => p.Class, "custom-input-control"));

        var div = comp.Find("div.mud-input-control");

        div.ClassList.Should().Contain("custom-input-control");
    }

    [Test]
    public void UserAttributes_ShouldBeSplattedOnTheRootElement()
    {
        var comp = Context.Render<MudInputControl>(parameters => parameters
            .AddUnmatched("data-test", "input-control-value"));

        var div = comp.Find("div.mud-input-control");

        div.GetAttribute("data-test").Should().Be("input-control-value");
    }
}
