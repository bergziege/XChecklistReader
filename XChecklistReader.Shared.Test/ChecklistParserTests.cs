using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Mock<IFileService> fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.ReadFileAsLines("")).Returns(new List<string>() {Checklist.KEYWORD});

            IChecklistParser parser = new ChecklistParser(fileServiceMock.Object);

            /* When: the line gets parsed */
            IList<Checklist> checklists = parser.ParseFromFile("");

            /* Then: I expect to get a single checklist without name or menue name */
            checklists.Count.Should().Be(1);
            checklists.First().Name.Should().BeEmpty();
            checklists.First().MenuName.Should().BeEmpty();
        }

        [Test]
        public void ChecklistLineWithSingleValueContentShouldGiveChecklistWithName()
        {
            /* Given: single checklist line but no content */
            Mock<IFileService> fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.ReadFileAsLines("")).Returns(new List<string>() { $"{Checklist.KEYWORD}test" });

            IChecklistParser parser = new ChecklistParser(fileServiceMock.Object);

            /* When: the line gets parsed */
            IList<Checklist> checklists = parser.ParseFromFile("");

            /* Then: I expect to get a single checklist without name or menue name */
            checklists.Count.Should().Be(1);
            checklists.First().Name.Should().Be("test");
            checklists.First().MenuName.Should().BeEmpty();
        }

        [Test]
        public void ChecklistLineWithDualValueContentShouldGiveChecklistWithNames()
        {
            /* Given: single checklist line but no content */
            Mock<IFileService> fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.ReadFileAsLines("")).Returns(new List<string>() { $"{Checklist.KEYWORD}Display name:Menu name" });

            IChecklistParser parser = new ChecklistParser(fileServiceMock.Object);

            /* When: the line gets parsed */
            IList<Checklist> checklists = parser.ParseFromFile("");

            /* Then: I expect to get a single checklist without name or menue name */
            checklists.Count.Should().Be(1);
            checklists.First().Name.Should().Be("Display name");
            checklists.First().MenuName.Should().Be("Menu name");
        }
    }
}