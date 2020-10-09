using System.Collections.Generic;

namespace XChecklistReader.Services.Domain {
    public class SimpleChecklistItem : ChecklistItemBase {
        public const string KEYWORD = "sw_item:";

        public SimpleChecklistItem(string description, string condition) {
            Description = description;
            if (!string.IsNullOrWhiteSpace(condition))
                Condition = condition;
            else
                Condition = "Checked";
            
        }

        public string Description { get; }
        public string Condition { get; }
    }
}