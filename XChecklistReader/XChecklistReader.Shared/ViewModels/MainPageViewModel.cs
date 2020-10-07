using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Uno.UI.Common;
using XChecklistReader.Services.Domain;
using XChecklistReader.Services.Service;
using XChecklistReader.Services.Service.Impl;

namespace XChecklistReader.ViewModels {
    public class MainPageViewModel : ViewModelBase {
        private readonly HttpClient _httpClient;
        
        public MainPageViewModel() {
            SelectFileCommand = new DelegateCommand(SelectFile);
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://checklists.berndnet-2000.de");
            LoadFiles();
        }

        public ObservableCollection<string> AvailableFiles { get; } = new ObservableCollection<string>();

        public string SelectedAvailableFile { get; set; }

        public ObservableCollection<Checklist> Checklists { get; } = new ObservableCollection<Checklist>();

        public DelegateCommand SelectFileCommand { get; }

        private async void LoadFiles() {
            var httpResponseMessage = await _httpClient.GetAsync("rest/checklist/file.php");
            if (httpResponseMessage.IsSuccessStatusCode) {
                var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                var availableFiles = JsonConvert.DeserializeObject<IList<string>>(jsonResponse);
                foreach (var availableFile in availableFiles) AvailableFiles.Add(availableFile);
            }
        }

        private async void SelectFile() {
            var httpResponseMessage = await _httpClient.GetAsync(SelectedAvailableFile);
            if (httpResponseMessage.IsSuccessStatusCode) {
                var fileContent = await httpResponseMessage.Content.ReadAsStringAsync();
                IList<string> lines = new List<string>();
                foreach (var line in fileContent.Split(Environment.NewLine)) lines.Add(line);

                IChecklistParser parser = new ChecklistParser();
                var checklists = await parser.ParseFromFile(lines);
                Checklists.Clear();
                foreach (var checklist in checklists) Checklists.Add(checklist);
            }
        }
    }
}