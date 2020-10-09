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
                    currentChecklist.AddItem(new SimpleChecklistItem(description, condition));
                }

                if (currentChecklist != null && line.StartsWith(VoidChecklistItem.KEYWORD)) {
                    currentChecklist.AddItem(new VoidChecklistItem(ParseVoidChecklistItem(line)));
                }
            }

            if (currentChecklist != null) checklists.Add(currentChecklist);

            return checklists;
        }

        private string ParseVoidChecklistItem(string line) {
            return line.Replace(VoidChecklistItem.KEYWORD, "");
        }

        private (string, string) ParseSImpleChecklistItem(string line) {
            string lineContent = line.Replace(SimpleChecklistItem.KEYWORD, "");
            if (!string.IsNullOrWhiteSpace(lineContent)) {
                string[] splitContent = lineContent.Split('|');
                string description = splitContent[0];
                if (splitContent.Length == 1) return (description, "");
                string condition = splitContent[1];
                if (condition.Contains(":")) {
                    condition = condition.Substring(0, condition.IndexOf(':'));
                }
                return (description, condition);
            }

            return ("", "");
        }

        private (string, string) ParseChecklistTitleLine(string line, string keyword) {
            var lineContent = line.Replace(Checklist.KEYWORD, "");
            if (lineContent != string.Empty) {
                var splitContent = lineContent.Split(':');
                string name = splitContent[0];
                if (splitContent.Length == 1) return (name, "");
                string menuName = splitContent[1];
                return (name, menuName);
            }

            return ("", "");
        }
    }
}