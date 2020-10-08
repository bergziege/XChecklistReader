namespace XChecklistReader.Services.Domain {
    public class SimpleChecklistItem : ChecklistItemBase {

        public const string KEYWORD = "sw_item:";

        public SimpleChecklistItem(string description, string condition = "Checked") {
            Description = description;
            Condition = condition;
        }

        public string Description { get; }
        public string Condition { get; }
    }
}