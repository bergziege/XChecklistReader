using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using XChecklistReader.Services.Domain;

namespace XChecklistReader.Services.Service.Impl {
    public class ChecklistParser : IChecklistParser {
 
        public async Task<IList<Checklist>> ParseFromFile(IList<string> lines) {
            
            IList<Checklist> checklists = new List<Checklist>();

            Checklist currentChecklist = null;

            foreach (var line in lines)
                if (line.StartsWith(Checklist.KEYWORD)) {
                    if (currentChecklist != null) checklists.Add(currentChecklist);

                    var values = GetDualValueAfterKeyword(line, Checklist.KEYWORD);
                    currentChecklist = new Checklist(values.Item1, values.Item2);
                }

            if (currentChecklist != null) {
                checklists.Add(currentChecklist);
            }

            return checklists;
        }

        private (string, string) GetDualValueAfterKeyword(string line, string keyword) {
            var lineContent = line.Replace(keyword, "");
            if (lineContent != string.Empty) {
                var splitContent = lineContent.Split(':');
                if (splitContent.Length == 1) {
                    return (splitContent[0], "");
                }
                return (splitContent[0], splitContent[1]);
            }

            return ("", "");
        }
    }
}