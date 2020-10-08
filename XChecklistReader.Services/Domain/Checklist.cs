using System.Collections.Generic;

namespace XChecklistReader.Services.Domain {
    public class Checklist {
        public const string KEYWORD = "sw_checklist:";

        public Checklist(string name, string menuName) {
            Name = name;
            MenuName = menuName;
        }

        public IList<ChecklistItemBase> ChecklistItems { get; } = new List<ChecklistItemBase>();

        public string Name { get; set; }
        public string MenuName { get; set; }

        public void AddItem(ChecklistItemBase itemToAdd) {
            ChecklistItems.Add(itemToAdd);
        }
    }
}