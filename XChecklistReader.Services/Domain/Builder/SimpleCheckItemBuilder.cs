namespace XChecklistReader.Services.Domain.Builder {
    public class SimpleCheckItemBuilder {
        private string _condition;
        private string _dataref;
        private string _datarefCondition;
        private string _description;

        public SimpleChecklistItem Build() {
            return new SimpleChecklistItem(_description, _condition, _dataref, _datarefCondition);
        }

        public SimpleCheckItemBuilder WithDescription(string description) {
            _description = description;
            return this;
        }

        public SimpleCheckItemBuilder WithCondition(string condition) {
            _condition = condition;
            return this;
        }

        public SimpleCheckItemBuilder WithDataref(string dataref) {
            _dataref = dataref;
            return this;
        }

        public SimpleCheckItemBuilder WithDatarefCondition(string datarefCondition) {
            _datarefCondition = datarefCondition;
            return this;
        }
    }
}