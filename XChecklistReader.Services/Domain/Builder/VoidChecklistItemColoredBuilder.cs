using System.Collections.Generic;
using Windows.UI;

namespace XChecklistReader.Services.Domain.Builder {
    public class VoidChecklistItemColoredBuilder {
        private IList<ColoredChecklistItemContentPart> _content;

        public VoidChecklistItemColored Build() {

            if (_content == null) {
                _content = new List<ColoredChecklistItemContentPart>();
                _content.Add(new ColoredChecklistItemContentPart(Colors.Brown, "Brown "));
                _content.Add(new ColoredChecklistItemContentPart(Colors.Green, "Green !"));
            }

            return new VoidChecklistItemColored(_content);
        }
    }
}