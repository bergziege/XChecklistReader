using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using XChecklistReader.Services.Domain;
using XChecklistReader.Services.Service;
using XChecklistReader.Services.Service.Impl;

namespace XChecklistReader.Shared.Test
{
    public class ChecklistParserTests
    {
        
        [Test]
        public void ChecklistLineWithoutContentShouldGiveChecklistWithoutNames()
        {
            /* Given: single checklist line but no content */
            IList<string> lines = new List<string>();
            lines.Add(Checklist.KEYWORD);

            IChecklistParser parser = new ChecklistParser();

            /* When: the line gets parsed */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then: I expect to get a single checklist without name or menue name */
            checklists.Count.Should().Be(1);
            checklists.First().Name.Should().BeEmpty();
            checklists.First().MenuName.Should().BeEmpty();
        }

        [Test]
        public void ChecklistLineWithSingleValueContentShouldGiveChecklistWithName()
        {
            /* Given: single checklist line but no content */
            IList<string> lines = new List<string>();
            lines.Add($"{Checklist.KEYWORD}test");

            IChecklistParser parser = new ChecklistParser();

            /* When: the line gets parsed */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then: I expect to get a single checklist without name or menue name */
            checklists.Count.Should().Be(1);
            checklists.First().Name.Should().Be("test");
            checklists.First().MenuName.Should().BeEmpty();
        }

        [Test]
        public void ChecklistLineWithDualValueContentShouldGiveChecklistWithNames()
        {
            /* Given: single checklist line but no content */
            IList<string> lines = new List<string>();
            lines.Add($"{Checklist.KEYWORD}Display name:Menu name");

            IChecklistParser parser = new ChecklistParser();

            /* When: the line gets parsed */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then: I expect to get a single checklist without name or menue name */
            checklists.Count.Should().Be(1);
            checklists.First().Name.Should().Be("Display name");
            checklists.First().MenuName.Should().Be("Menu name");
        }

        [Test]
        public void ChecklistItemInChecklistShouldHaveDescription() {
            /* Given */
            IList<string> lines = new List<string>();
            lines.Add($"{Checklist.KEYWORD}");
            lines.Add($"{SimpleChecklistItem.KEYWORD}Gear lever");

            IChecklistParser parser = new ChecklistParser();

            /* When */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then */
            checklists.Single().ChecklistItems.Single().As<SimpleChecklistItem>().Description.Should().Be("Gear lever");
        }

        [Test]
        public void ChecklistItemInChecklistShouldHaveDefaultCondition()
        {
            /* Given */
            IList<string> lines = new List<string>();
            lines.Add($"{Checklist.KEYWORD}");
            lines.Add($"{SimpleChecklistItem.KEYWORD}Gear lever");

            IChecklistParser parser = new ChecklistParser();

            /* When */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then */
            checklists.Single().ChecklistItems.Single().As<SimpleChecklistItem>().Condition.Should().Be("Checked");
        }

        [Test]
        public void ChecklistItemInChecklistShouldHaveCustomCondition()
        {
            /* Given */
            IList<string> lines = new List<string>();
            lines.Add($"{Checklist.KEYWORD}");
            lines.Add($"{SimpleChecklistItem.KEYWORD}Gear lever|down");

            IChecklistParser parser = new ChecklistParser();

            /* When */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then */
            checklists.Single().ChecklistItems.Single().As<SimpleChecklistItem>().Condition.Should().Be("down");
        }

        [Test]
        public void ChecklistItemInChecklistShouldNotHaveDatarefAfterCondition()
        {
            /* Given */
            IList<string> lines = new List<string>();
            lines.Add($"{Checklist.KEYWORD}");
            lines.Add($"{SimpleChecklistItem.KEYWORD}Gear lever|down:my/dataref:>1");

            IChecklistParser parser = new ChecklistParser();

            /* When */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then */
            checklists.Single().ChecklistItems.Single().As<SimpleChecklistItem>().Condition.Should().Be("down");
        }

        [Test]
        public void VoidItemWithoutContentShouldHaveEmptyDescription() {
            /* Given */
            IList<string> lines = new List<string>();
            lines.Add($"{Checklist.KEYWORD}");
            lines.Add($"{VoidChecklistItem.KEYWORD}");

            IChecklistParser parser = new ChecklistParser();

            /* When */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then */
            checklists.Single().ChecklistItems.Single().As<VoidChecklistItem>().Description.Should().BeEmpty();
        }

        [Test]
        public void VoidItemWithContentShouldHaveDescription()
        {
            /* Given */
            IList<string> lines = new List<string>();
            lines.Add($"{Checklist.KEYWORD}");
            lines.Add($"{VoidChecklistItem.KEYWORD}Section A : and B ?");

            IChecklistParser parser = new ChecklistParser();

            /* When */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then */
            checklists.Single().ChecklistItems.Single().As<VoidChecklistItem>().Description.Should().Be("Section A : and B ?");
        }

        [Test]
        public void ColoredItemShouldHaveCorrectlyColoredParts()
        {
            /* Given */
            IList<string> lines = new List<string>();
            lines.Add($"{ChecklistParser.KEYWORD_COLOR_DEF}red:1.0,0.0,0.0");
            lines.Add($"{ChecklistParser.KEYWORD_COLOR_DEF}grey:0.8,0.8,0.8");
            lines.Add($"{Checklist.KEYWORD}");
            lines.Add(@$"{VoidChecklistItemColored.KEYWORD}\red\Section \grey\A \red\and \grey\B ?");
            Color red = Color.FromArgb(255, 255, 0, 0);
            Color grey = Color.FromArgb(255, 204, 204, 204);

            IChecklistParser parser = new ChecklistParser();

            /* When */
            IList<Checklist> checklists = parser.ParseLines(lines);

            /* Then */
            VoidChecklistItemColored voidChecklistItemColored = checklists.Single().ChecklistItems.Single().As<VoidChecklistItemColored>();
            voidChecklistItemColored.Content.Count.Should().Be(4);
            voidChecklistItemColored.Content[0].Color.Should().Be(red);
            voidChecklistItemColored.Content[1].Color.Should().Be(grey);
            voidChecklistItemColored.Content[2].Color.Should().Be(red);
            voidChecklistItemColored.Content[3].Color.Should().Be(grey);

            voidChecklistItemColored.Content[0].Content.Should().Be("Section ");
            voidChecklistItemColored.Content[1].Content.Should().Be("A ");
            voidChecklistItemColored.Content[2].Content.Should().Be("and ");
            voidChecklistItemColored.Content[3].Content.Should().Be("B ?");
        }
    }
}