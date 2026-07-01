// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using MudBlazor.UnitTests.TestComponents.Card;
using NUnit.Framework;

namespace MudBlazor.UnitTests.Components
{
    [TestFixture]
    public class CardTests : BunitTest
    {
        [Test]
        public async Task CardChildContent()
        {
            //Card header with child content should be render successfully
            var comp = Context.Render<CardChildContentTest>();
            var button = comp.FindComponent<MudButton>();
            var numeric = comp.FindComponent<MudNumericField<int>>();
            await comp.WaitForAssertionAsync(() => numeric.Instance.Value.Should().Be(0));
            await comp.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
            await comp.WaitForAssertionAsync(() => numeric.Instance.Value.Should().Be(1));
        }

        [Test]
        public void MudCard_Defaults_ShouldExposeExpectedParameterValues()
        {
            var comp = Context.Render<MudCard>();

            comp.Instance.Elevation.Should().Be(1);
            comp.Instance.Square.Should().BeFalse();
            comp.Instance.Outlined.Should().BeFalse();
            comp.Instance.ContentPadding.Should().BeTrue();
        }

        [Test]
        public void MudCard_ShouldRenderWithBaseClass()
        {
            var comp = Context.Render<MudCard>();

            comp.Find(".mud-card").Should().NotBeNull();
        }

        [Test]
        public void MudCard_CustomClass_ShouldBeAppended()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .Add(p => p.Class, "my-card"));

            comp.Find(".mud-card").ClassList.Should().Contain("my-card");
        }

        [Test]
        public void MudCard_UserAttributes_ShouldBeSplattedOnTheRootElement()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .AddUnmatched("data-test", "card-value"));

            comp.Find(".mud-card").GetAttribute("data-test").Should().Be("card-value");
        }

        [Test]
        public void MudCardContent_ShouldRenderWithPaddingClassByDefault()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .AddChildContent<MudCardContent>(child => child
                    .AddChildContent("Card body")));

            var content = comp.Find(".mud-card-content");

            content.ClassList.Should().Contain("mud-card-content-padding");
            content.TextContent.Should().Contain("Card body");
        }

        [Test]
        public void MudCardContent_ShouldOmitPaddingClassWhenDisabled()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .Add(p => p.ContentPadding, false)
                .AddChildContent<MudCardContent>(child => child
                    .AddChildContent("No padding")));

            var content = comp.Find(".mud-card-content");

            content.ClassList.Should().NotContain("mud-card-content-padding");
        }

        [Test]
        public void MudCardActions_ShouldRenderWithPaddingClassByDefault()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .AddChildContent<MudCardActions>(child => child
                    .AddChildContent("Actions")));

            var actions = comp.Find(".mud-card-actions");

            actions.ClassList.Should().Contain("mud-card-actions-padding");
            actions.TextContent.Should().Contain("Actions");
        }

        [Test]
        public void MudCardActions_ShouldOmitPaddingClassWhenDisabled()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .Add(p => p.ContentPadding, false)
                .AddChildContent<MudCardActions>(child => child
                    .AddChildContent("No padding")));

            var actions = comp.Find(".mud-card-actions");

            actions.ClassList.Should().NotContain("mud-card-actions-padding");
        }

        [Test]
        public void MudCardHeader_ShouldRenderWithPaddingClassByDefault()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .AddChildContent<MudCardHeader>(child => child
                    .Add(p => p.CardHeaderContent, (RenderFragment)(builder =>
                    {
                        builder.AddContent(0, "Header text");
                    }))));

            var header = comp.Find(".mud-card-header");

            header.ClassList.Should().Contain("mud-card-header-padding");
        }

        [Test]
        public void MudCardHeader_ShouldOmitPaddingClassWhenDisabled()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .Add(p => p.ContentPadding, false)
                .AddChildContent<MudCardHeader>(child => child
                    .Add(p => p.CardHeaderContent, (RenderFragment)(builder =>
                    {
                        builder.AddContent(0, "Header");
                    }))));

            var header = comp.Find(".mud-card-header");

            header.ClassList.Should().NotContain("mud-card-header-padding");
        }

        [Test]
        public void MudCardHeader_ShouldRenderAvatarContentAndActions()
        {
            var comp = Context.Render<MudCard>(parameters => parameters
                .AddChildContent<MudCardHeader>(child => child
                    .Add(p => p.CardHeaderAvatar, (RenderFragment)(b => b.AddContent(0, "AV")))
                    .Add(p => p.CardHeaderContent, (RenderFragment)(b => b.AddContent(0, "Title")))
                    .Add(p => p.CardHeaderActions, (RenderFragment)(b => b.AddContent(0, "Act")))));

            comp.Find(".mud-card-header-avatar").TextContent.Should().Contain("AV");
            comp.Find(".mud-card-header-content").TextContent.Should().Contain("Title");
            comp.Find(".mud-card-header-actions").TextContent.Should().Contain("Act");
        }

        [Test]
        public void MudCardMedia_Defaults_ShouldHaveExpectedHeight()
        {
            var comp = Context.Render<MudCardMedia>();

            comp.Instance.Height.Should().Be(300);
            comp.Instance.Title.Should().BeNull();
            comp.Instance.Image.Should().BeNull();
        }

        [Test]
        public void MudCardMedia_ShouldRenderBackgroundImageStyle()
        {
            var comp = Context.Render<MudCardMedia>(parameters => parameters
                .Add(p => p.Image, "test.jpg")
                .Add(p => p.Height, 200)
                .Add(p => p.Title, "Test Image"));

            var div = comp.Find("div.mud-card-media");

            div.GetAttribute("style").Should().Contain("background-image:url(\"test.jpg\")");
            div.GetAttribute("style").Should().Contain("height: 200px");
            div.GetAttribute("title").Should().Be("Test Image");
        }
    }
}
