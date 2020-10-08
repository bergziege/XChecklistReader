using System.Collections.ObjectModel;
using Uno.UI.Common;
using XChecklistReader.Services.Domain;

namespace UI.Main {
    public interface IMainPageViewModel {
        ObservableCollection<string> AvailableFiles { get; }
        string SelectedAvailableFile { get; set; }
        ObservableCollection<Checklist> Checklists { get; }
        DelegateCommand SelectFileCommand { get; }
    }
}