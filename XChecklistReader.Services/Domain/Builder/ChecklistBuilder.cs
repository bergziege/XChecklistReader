namespace XChecklistReader.Services.Domain.Builder {
    public class ChecklistBuilder {
        private string _menuName;
        private string _name;

        public Checklist Build() {
            return new Checklist(_name, _menuName);
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