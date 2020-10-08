using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XChecklistReader.Services.Domain;

namespace XChecklistReader.UI.Main.TemplateSelectors {
    public class ChecklistItemTemplateSelector : DataTemplateSelector {

        public DataTemplate SimpleChecklistItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            if (item is SimpleChecklistItem) {
                return SimpleChecklistItemTemplate;
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}