namespace XChecklistReader.Services.Domain.Builder {
    public class SimpleCheckItemBuilder {
        private string _condition;
        private string _description;

        public SimpleChecklistItem Build() {
            return new SimpleChecklistItem(_description, _condition);
        }

        public SimpleCheckItemBuilder WithDescription(string description) {
            _description = description;
            return this;
        }

        public SimpleCheckItemBuilder WithCondition(string condition) {
            _condition = condition;
            return this;
        }
    }
}