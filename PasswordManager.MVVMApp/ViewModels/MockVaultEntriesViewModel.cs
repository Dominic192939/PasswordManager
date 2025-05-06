using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PasswordManager.Common.Modules.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.MVVMApp.ViewModels
{
    public partial class MockVaultEntriesViewModel : ViewModelBase
    {
        public MockVaultEntriesViewModel()
        {
            AddItemCommand = new RelayCommand(OnAddItemAsync);
            LoadModelsCommand = new AsyncRelayCommand(OnLoadModelsAsync);
            EditItemCommand = new RelayCommand<MockVaultEntryViewModel>(OnEditItemAsync);
            DeleteItemCommand = new RelayCommand<MockVaultEntryViewModel>(OnDeleteItemAsync);
            Models = new ObservableCollection<MockVaultEntryViewModel>();

            _ = OnLoadModelsAsync();
        }
        #region Properties
        private string _filter;
        private MockVaultEntryViewModel _selectedItem;
        private ObservableCollection<MockVaultEntryViewModel> _models;
        private ObservableCollection<MockVaultEntryViewModel> _filteredModels;

        public ObservableCollection<MockVaultEntryViewModel> Models
        {
            get => _models;
            private set => SetProperty(ref _models, value);
        }

        public ObservableCollection<MockVaultEntryViewModel> FilteredModels
        {
            get => _filteredModels;
            private set => SetProperty(ref _filteredModels, value);
        }

        public MockVaultEntryViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        public string Filter
        {
            get => _filter;
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged();
                    ApplyFilter();
                }
            }
        }
        #endregion

        #region Commands
        public ICommand AddItemCommand { get; }
        public ICommand LoadModelsCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        #endregion Commands

        #region Command Methods
        private async void OnAddItemAsync()
        {
            var vm = new MockVaultEntryViewModel
            {
                Name = "Neuer Eintrag",
                UserName = "Benutzer",
                Password = "Passwort"
            };

            string url = "https://localhost:7203/api/MockVaultEntries";
            try
            {
                using var client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(vm), System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var created = JsonConvert.DeserializeObject<Models.MockVaultEntry>(json);

                Models.Add(vm);
                ApplyFilter();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Erstellen: {ex.Message}");
            }
        }

        private async Task OnLoadModelsAsync()
        {
            string url = "https://localhost:7203/api/MockVaultEntries";

            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var entries = JsonConvert.DeserializeObject<List<Models.MockVaultEntry>>(json);

                Models.Clear();
                foreach (var entry in entries)
                {
                    Models.Add(new MockVaultEntryViewModel
                    {
                        Name = entry.Name,
                        UserName = entry.UserName,
                        Password = entry.Password,
                        Url = entry.Url,
                        Email = entry.Email
                    });
                }

                ApplyFilter();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden: {ex.Message}");
            }
        }


        private async void OnEditItemAsync(MockVaultEntryViewModel entry)
        {
            if (entry == null || entry.Guid == Guid.Empty) return;

            string url = $"https://localhost:7203/api/MockVaultEntries/{entry.Guid}";

            try
            {
                using var client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(entry), System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();

                // Optional: Result aus Response aktualisieren
                var json = await response.Content.ReadAsStringAsync();
                var updated = JsonConvert.DeserializeObject<Models.MockVaultEntry>(json);

                ApplyFilter();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Bearbeiten: {ex.Message}");
            }
        }

        private async void OnDeleteItemAsync(MockVaultEntryViewModel entry)
        {
            if (entry == null || entry.Guid == Guid.Empty) return;

            string url = $"https://localhost:7203/api/MockVaultEntries/{entry.Guid}";

            try
            {
                using var client = new HttpClient();
                var response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();

                Models.Remove(entry);
                ApplyFilter();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Löschen: {ex.Message}");
            }
        }
        #endregion

        #region Filter Logic
        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(Filter))
            {
                FilteredModels = new ObservableCollection<MockVaultEntryViewModel>(Models);
            }
            else
            {
                var lower = Filter.ToLowerInvariant();
                var filtered = Models.Where(m =>
                    (!string.IsNullOrEmpty(m.Name) && m.Name.ToLowerInvariant().Contains(lower)) ||
                    (!string.IsNullOrEmpty(m.UserName) && m.UserName.ToLowerInvariant().Contains(lower))
                );
                FilteredModels = new ObservableCollection<MockVaultEntryViewModel>(filtered);
            }
        }
        #endregion



        /// <summary>
        /// Initializes the class (created by the generator).
        /// </summary>
        static MockVaultEntriesViewModel()
        {
            ClassConstructing();
            ClassConstructed();
        }
        /// <summary>
        /// This method is called before the construction of the class.
        /// </summary>
        static partial void ClassConstructing();
        /// <summary>
        /// This method is called when the class is constructed.
        /// </summary>
        static partial void ClassConstructed();

        /// <summary>
        /// This method is called the object is being constraucted.
        /// </summary>
        partial void Constructing();
        /// <summary>
        /// This method is called when the object is constructed.
        /// </summary>
        partial void Constructed();
    }
}