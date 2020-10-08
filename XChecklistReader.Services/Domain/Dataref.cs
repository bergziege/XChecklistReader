namespace XChecklistReader.Services.Domain {
    public class Dataref : DatarefPartBase {
        public string Name { get; }
        public DatarefCondition Condition { get; }
    }
}