using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PasswordManager.MVVMApp.Models;
using PasswordManager.MVVMApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.MVVMApp.ViewModels
{
    public partial class TESTVIEWMODEL : ViewModelBase
    {
        #region fields
        private string _filter = string.Empty;
        private MockVaultEntry selectedItem;
        private readonly List<MockVaultEntry> _entries = [];       
        #endregion fields

        #region properties
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                ApplyFilter(value);
                OnPropertyChanged();
            }
        }
        public MockVaultEntry SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MockVaultEntry> Entries { get; } = [];
        #endregion properties

        public TESTVIEWMODEL()
        {
            _ = LoadCompaniesAsync();
        }

        #region commands
        [RelayCommand]
        public async Task LoadItems()
        {
            await LoadCompaniesAsync();
        }
        [RelayCommand]
        public async Task AddItem()
        {
            //var companyWindow = new MockVaultEntryWindow();
           // var viewModel = new MockVaultEntryViewModel { CloseAction = companyWindow.Close };

            //companyWindow.DataContext = viewModel;
            // Aktuelles Hauptfenster als Parent setzen
            var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
             //   await companyWindow.ShowDialog(mainWindow);
                _ = LoadCompaniesAsync();
            }
        }
        [RelayCommand]
        public async Task EditItem(MockVaultEntry entry)
        {
           // var companyWindow = new MockVaultEntryWindow();
            //var viewModel = new MockVaultEntryViewModel { Model = entry, CloseAction = companyWindow.Close };

           // companyWindow.DataContext = viewModel;
            // Aktuelles Hauptfenster als Parent setzen
            var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

            if (mainWindow != null)
            {
            //    await companyWindow.ShowDialog(mainWindow);
                _ = LoadCompaniesAsync();
            }
        }
        [RelayCommand]
        public async Task DeleteItem(MockVaultEntry entry)
        {
            var messageDialog = new MessageDialog("Delete", $"Wollen Sie die Firma '{entry.Name}' löschen?", MessageType.Question);
            var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

            // Aktuelles Hauptfenster als Parent setzen
            await messageDialog.ShowDialog(mainWindow!);

            if (messageDialog.Result == MessageResult.Yes)
            {
                using var httpClient = new HttpClient { BaseAddress = new Uri(API_BASE_URL) };


                var response = await httpClient.DeleteAsync($"MockVaultEntries/{entry.Guid}");

                if (response.IsSuccessStatusCode == false)
                {
                    messageDialog = new MessageDialog("Error", "Beim Löschen ist ein Fehler aufgetreten!", MessageType.Error);
                    await messageDialog.ShowDialog(mainWindow!);
                }
                else
                {
                    _ = LoadCompaniesAsync();
                }
            }
        }
        #endregion commands

        private async void ApplyFilter(string filter)
        {
            // UI-Update sicherstellen
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var selectedItem = SelectedItem;

                Entries.Clear();
                foreach (var company in _entries)
                {
                    if (company.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase))
                    {
                        Entries.Add(company);
                    }
                }
                if (selectedItem != null)
                {
                    SelectedItem = Entries.FirstOrDefault(e => e.Guid == selectedItem.Guid);
                }
            });
        }
        private async Task LoadCompaniesAsync()
        {
            try
            {
                using var httpClient = new HttpClient { BaseAddress = new Uri(API_BASE_URL) };
                var response = await httpClient.GetStringAsync("MockVaultEntries");
                var companies = JsonConvert.DeserializeObject<List<MockVaultEntry>>(response);

                if (companies != null)
                {
                    _entries.Clear();
                    foreach (var company in companies)
                    {
                        _entries.Add(company);
                    }
                    ApplyFilter(Filter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading companies: {ex.Message}");
            }
        }
    }
}