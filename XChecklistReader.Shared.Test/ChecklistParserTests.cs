using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace XChecklistReader.Shared.Test
{
    public class ChecklistParserTests
    {
        

        [Test]
        public async Task ChecklistLineWithoutContentShouldGiveChecklistWithoutNames()
        {
            /* Given: single checklist line but no content */
            Mock<IFileService> fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.ReadFileAsLinesAsync("")).ReturnsAsync(new List<string>() {Checklist.KEYWORD});

            IChecklistParser parser = new ChecklistParser(fileServiceMock.Object);

            /* When: the line gets parsed */
            IList<Checklist> checklists = await parser.ParseFromFileAsync("");

            /* Then: I expect to get a single checklist without name or menue name */
            checklists.Count.Should().Be(1);
            checklists.First().Name.Should().BeEmpty();
            checklists.First().MenuName.Should().BeEmpty();
        }
    }
}