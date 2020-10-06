namespace XChecklistReader.Services.Domain {
    public class Checklist {
        public Checklist(string name, string menuName) {
            Name = name;
            MenuName = menuName;
        }

        public const string KEYWORD = "sw_checklist:";

        public string Name { get; set; }
        public string MenuName { get; set; }
    }
}