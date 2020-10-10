using System.Collections.Generic;

namespace XChecklistReader.Services.Domain {
    public class VoidChecklistItemColored : ChecklistItemBase {
        public const string KEYWORD = "sw_itemvoid_c:";

        public VoidChecklistItemColored(IList<ColoredChecklistItemContentPart> content) {
            Content = content;
        }

        public IList<ColoredChecklistItemContentPart> Content { get; }
    }
}