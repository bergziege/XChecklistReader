using System.Collections.Generic;
using XChecklistReader.Services.Domain;

namespace XChecklistReader.Services.Service.Impl {
    public class ChecklistParser : IChecklistParser {
        public IList<Checklist> ParseLines(IList<string> lines) {
            IList<Checklist> checklists = new List<Checklist>();

            Checklist currentChecklist = null;

            foreach (var line in lines) {
                if (line.StartsWith(Checklist.KEYWORD)) {
                    if (currentChecklist != null) checklists.Add(currentChecklist);

                    (var name, var menuName) = ParseChecklistTitleLine(line, Checklist.KEYWORD);
                    currentChecklist = new Checklist(name, menuName);
                }

                if (currentChecklist != null && line.StartsWith(SimpleChecklistItem.KEYWORD)) {
                    (var description, var condition) = ParseSImpleChecklistItem(line);
                    if (!string.IsNullOrWhiteSpace(condition))
                        currentChecklist.AddItem(new SimpleChecklistItem(description, condition));
                    else
                        currentChecklist.AddItem(new SimpleChecklistItem(description));
                }
            }

            if (currentChecklist != null) checklists.Add(currentChecklist);

            return checklists;
        }

        private (string, string) ParseSImpleChecklistItem(string line) {
            string lineContent = line.Replace(SimpleChecklistItem.KEYWORD, "");
            if (!string.IsNullOrWhiteSpace(lineContent)) {
                string[] splitContent = lineContent.Split('|');
                if (splitContent.Length == 1) return (splitContent[0], "");
                return (splitContent[0], splitContent[1]);
            }

            return ("", "");
        }

        private (string, string) ParseChecklistTitleLine(string line, string keyword) {
            var lineContent = line.Replace(Checklist.KEYWORD, "");
            if (lineContent != string.Empty) {
                var splitContent = lineContent.Split(':');
                if (splitContent.Length == 1) return (splitContent[0], "");
                return (splitContent[0], splitContent[1]);
            }

            return ("", "");
        }
    }
}