using System.Collections.ObjectModel;
using System.Linq;
using UI.Main;
using Uno.UI.Common;
using XChecklistReader.Services.Domain;
using XChecklistReader.Services.Domain.Builder;

namespace XChecklistReader.UI.Main.DesignViewModels {
    public class MainPageDesignViewModel : IMainPageViewModel {
        public MainPageDesignViewModel() {
            AvailableFiles.Add("someFile.txt");
            SelectedAvailableFile = AvailableFiles.First();
            Checklists.Add(new ChecklistBuilder().WithName("Preflight").WithMenuName("Preflight menu name").Build());
            Checklists.Add(new ChecklistBuilder().WithName("Climb").WithMenuName("Climb menu name").Build());
            Checklists.Add(new ChecklistBuilder().WithName("Cruise").WithMenuName("Cruise menu name").Build());
        }

        public ObservableCollection<string> AvailableFiles { get; } = new ObservableCollection<string>();
        public string SelectedAvailableFile { get; set; }
        public ObservableCollection<Checklist> Checklists { get; } = new ObservableCollection<Checklist>();
        public DelegateCommand SelectFileCommand { get; }
    }
}