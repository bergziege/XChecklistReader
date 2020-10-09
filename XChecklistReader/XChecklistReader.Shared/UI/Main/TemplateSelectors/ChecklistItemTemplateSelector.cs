using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XChecklistReader.Services.Domain;

namespace XChecklistReader.UI.Main.TemplateSelectors {
    public class ChecklistItemTemplateSelector : DataTemplateSelector {

        public DataTemplate SimpleChecklistItemTemplate { get; set; }

        public DataTemplate VoidItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            if (item is SimpleChecklistItem) {
                return SimpleChecklistItemTemplate;
            }

            if (item is VoidChecklistItem) {
                return VoidItemTemplate;
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}