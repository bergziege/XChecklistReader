namespace XChecklistReader.Services.Domain {
    public class VoidChecklistItem : ChecklistItemBase {
        public const string KEYWORD = "sw_itemvoid:";

        public VoidChecklistItem(string description) {
            Description = description;
        }

        public string Description { get; }
    }
}