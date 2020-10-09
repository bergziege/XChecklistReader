using System.Collections.Generic;
using Uno.Extensions;

namespace XChecklistReader.Services.Domain.Builder {
    public class ChecklistBuilder {
        private readonly IList<ChecklistItemBase> _items = new List<ChecklistItemBase>();
        private string _menuName;
        private string _name;

        public Checklist Build() {
            if (_items.Empty()) {
                SimpleCheckItemBuilder itemBuilder = new SimpleCheckItemBuilder();
                VoidChecklistItemBuilder voidBuilder = new VoidChecklistItemBuilder();
                _items.Add(itemBuilder.WithDescription("Check item 1").WithCondition("Down").Build());
                _items.Add(voidBuilder.WithDescription("Some descriptive void item").Build());
                _items.Add(itemBuilder.WithDescription("Check item 2").Build());
                _items.Add(itemBuilder.WithDescription("Check item 3").WithCondition("Up").Build());
            }

            Checklist checklist = new Checklist(_name, _menuName);
            foreach (var checklistItem in _items) {
                checklist.AddItem(checklistItem);
            }
            return checklist;
        }

        public ChecklistBuilder WithName(string name) {
            _name = name;
            return this;
        }

        public ChecklistBuilder WithMenuName(string menuName) {
            _menuName = menuName;
            return this;
        }
    }
}