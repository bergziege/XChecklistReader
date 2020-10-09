using System.Collections.Generic;

namespace XChecklistReader.Services.Domain {
    public class VoidChecklistItemColored {
        public IList<ColoredChecklistItemContentPart> Content { get; }

        public VoidChecklistItemColored(IList<ColoredChecklistItemContentPart> content) {
            Content = content;
        }
    }
}