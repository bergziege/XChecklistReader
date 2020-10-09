using Windows.UI;

namespace XChecklistReader.Services.Domain {
    public class ColoredChecklistItemContentPart {
        public ColoredChecklistItemContentPart(Color color, string content) {
            Color = color;
            Content = content;
        }

        public Color Color { get; }
        public string Content { get; }
    }
}