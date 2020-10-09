namespace XChecklistReader.Services.Domain.Builder {
    public class VoidChecklistItemBuilder {
        private string _description;

        public VoidChecklistItem Build() {
            return new VoidChecklistItem(_description);
        }

        public VoidChecklistItemBuilder WithDescription(string description) {
            _description = description;
            return this;
        }
    }
}