using System.Collections.Generic;
using System.Threading.Tasks;
using XChecklistReader.Services.Domain;

namespace XChecklistReader.Services.Service.Impl {
    public class ChecklistParser : IChecklistParser {
        private readonly IFileService _fileService;

        public ChecklistParser(IFileService fileService) {
            _fileService = fileService;
        }

        public IList<Checklist> ParseFromFile(string filePath) {
            var lines =  _fileService.ReadFileAsLines(filePath);

            IList<Checklist> checklists = new List<Checklist>();

            Checklist currentChecklist = null;

            foreach (var line in lines)
                if (line.StartsWith(Checklist.KEYWORD)) {
                    if (currentChecklist != null) checklists.Add(currentChecklist);

                    var values = GetDualValueAfterKeyword(line, Checklist.KEYWORD);
                    currentChecklist = new Checklist(values.Item1, values.Item2);
                }

            return checklists;
        }

        private (string, string) GetDualValueAfterKeyword(string line, string keyword) {
            var lineContent = line.Substring(0, keyword.Length);
            var splitContent = lineContent.Split(':');
            return (splitContent[0], splitContent[1]);
        }
    }
}