using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI;
using XChecklistReader.Services.Domain;

namespace XChecklistReader.Services.Service.Impl {
    public class ChecklistParser : IChecklistParser {
        public const string KEYWORD_COLOR_DEF = "sw_define_colour:";
        private const string DEFAULT_COLOR_NAME = "-reader-default-color-";

        public IList<Checklist> ParseLines(IList<string> lines) {
            IList<Checklist> checklists = new List<Checklist>();

            IDictionary<string, Color> colors = new Dictionary<string, Color>();
            colors.Add(DEFAULT_COLOR_NAME, Colors.DarkGray);
            
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

                if (line.StartsWith(KEYWORD_COLOR_DEF)) {
                    ParseColorDefinition(line, colors);
                }

                if (currentChecklist != null && line.StartsWith(VoidChecklistItemColored.KEYWORD)) {
                    currentChecklist.AddItem(new VoidChecklistItemColored(ParseVoidChecklistItemColored(line, colors)));
                }
            }

            if (currentChecklist != null) checklists.Add(currentChecklist);

            return checklists;
        }

        private IList<ColoredChecklistItemContentPart> ParseVoidChecklistItemColored(string line,
            IDictionary<string, Color> colors) {
            IList<ColoredChecklistItemContentPart> parts = new List<ColoredChecklistItemContentPart>();
            string content = line.Replace(VoidChecklistItemColored.KEYWORD, "");
            if (!string.IsNullOrEmpty(content)) {
                string[] contentElements = Regex.Split(content, @"(\\[a-z]*\\)").Where(x=>!string.IsNullOrEmpty(x)).ToArray();
                string currentColor = "";
                string currentContent = "";
                foreach (string element in contentElements) {
                    if (Regex.IsMatch(element,@"\\[a-z]*\\")) {
                        string colorName = element.Replace("\\", "");
                        if (colors.ContainsKey(colorName)) {
                            currentColor = colorName;
                        }
                        else {
                            currentColor = DEFAULT_COLOR_NAME;
                        }
                    }
                    else {
                        currentContent = element;
                    }

                    if (!string.IsNullOrEmpty(currentContent)) {
                        if (string.IsNullOrEmpty(currentColor)) {
                            currentColor = DEFAULT_COLOR_NAME;
                        }

                        Color partColor = colors[currentColor]; ;
                        
                        parts.Add(new ColoredChecklistItemContentPart(partColor, currentContent));
                        currentColor = "";
                        currentContent = "";
                    }
                }
            }

            return parts;
        }

        private void ParseColorDefinition(string line, IDictionary<string, Color> colors) {
            string colorDef = line.Replace(KEYWORD_COLOR_DEF, "");
            if (colorDef != string.Empty) {
                string[] nameAndValue = colorDef.Split(':');
                if (nameAndValue.Length == 2) {
                    if (colors.ContainsKey(nameAndValue[0])) {
                        return;
                    }
                    string[] colorValues = nameAndValue[1].Split(',');
                    bool hasR = double.TryParse(colorValues[0],NumberStyles.Float, CultureInfo.InvariantCulture, out double r);
                    bool hasG = double.TryParse(colorValues[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double g);
                    bool hasB = double.TryParse(colorValues[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double b);
                    if (hasR && hasG && hasB) {
                        colors.Add(nameAndValue[0], Color.FromArgb(255,(byte)(r*255), (byte)(g*255), (byte)(b*255)));
                    }
                }
            }
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