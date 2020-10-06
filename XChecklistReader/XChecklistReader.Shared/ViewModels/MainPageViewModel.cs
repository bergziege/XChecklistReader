using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.Storage.Pickers;
using Uno.UI.Common;
using XChecklistReader.Services.Domain;
using XChecklistReader.Services.Service;
using XChecklistReader.Services.Service.Impl;


namespace XChecklistReader.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _selectedFilePath = "";

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

        public ObservableCollection<Checklist> Checklists { get; private set; } = new ObservableCollection<Checklist>();

        public DelegateCommand SelectFileCommand { get; }

        private async void SelectFile() {
            FileOpenPicker fileOpenPicker = new Windows.Storage.Pickers.FileOpenPicker();
            fileOpenPicker.ViewMode = PickerViewMode.List;
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            fileOpenPicker.FileTypeFilter.Add(".txt");
            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            if (file != null) {
                SelectedFilePath = file.Path; 
                IChecklistParser parser = new ChecklistParser(new FileService());
                IList<Checklist> checklists = await parser.ParseFromFile(file);
                Checklists.Clear();
                foreach (Checklist checklist in checklists) {
                    Checklists.Add(checklist);
                }
            }
        }
    }
}