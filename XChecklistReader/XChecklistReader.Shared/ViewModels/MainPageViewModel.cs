using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Uno.UI.Common;


namespace XChecklistReader.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _selectedFilePath = "42";

        public MainPageViewModel() {
            SelectFileCommand = new DelegateCommand(SelectFile);
        }

        public string SelectedFilePath
        {
            get => _selectedFilePath;
            private set {
                _selectedFilePath = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand SelectFileCommand { get; }

        private async void SelectFile() {
            FileOpenPicker fileOpenPicker = new Windows.Storage.Pickers.FileOpenPicker();
            fileOpenPicker.ViewMode = PickerViewMode.List;
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            fileOpenPicker.FileTypeFilter.Add(".txt");
            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            if (file != null) {
                SelectedFilePath = file.Path; 
            }
        }
    }
}