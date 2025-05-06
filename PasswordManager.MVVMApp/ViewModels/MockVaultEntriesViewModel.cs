using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.MVVMApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace PasswordManager.MVVMApp.ViewModels
{
    public partial class MockVaultEntriesViewModel : ViewModelBase
    {
        #region fields
        private string _filter = string.Empty;
        private Models.MockVaultEntry? selectedItem;
        private List<Models.MockVaultEntry> _entities = [];
        #endregion fields

        #region properties
        public ObservableCollection<Models.MockVaultEntry> Entities { get; } = [];
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
        public Models.MockVaultEntry SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }
        #endregion properties

        #region commands
        [RelayCommand]
        public async Task LoadItems()
        {
            await LoadEntitiesAsync();
        }
        [RelayCommand]
        public async Task AddItem()
        {
            var entiteWindow = new MockEntryWindow();
            var viewModel = new MockVaultEntryViewModel { CloseAction = entiteWindow.Close };

            entiteWindow.DataContext = viewModel;
            // Aktuelles Hauptfenster als Parent setzen
            var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
                await entiteWindow.ShowDialog(mainWindow);
                _ = LoadEntitiesAsync();
            }
        }
        [RelayCommand]
        public async Task EditItem(Models.MockVaultEntry entity)
        {
            var entityWindow = new MockEntryWindow();
            var viewModel = new MockVaultEntryViewModel { Model = entity, CloseAction = entityWindow.Close };

            entityWindow.DataContext = viewModel;
            // Aktuelles Hauptfenster als Parent setzen
            var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

            if (mainWindow != null)
            {
                await entityWindow.ShowDialog(mainWindow);
                _ = LoadEntitiesAsync();
            }
        }
        [RelayCommand]
        public async Task DeleteItem(Models.MockVaultEntry entity)
        {
            var messageDialog = new MessageDialog("Delete", $"Wollen Sie die Entität '{entity.Name}' löschen?", MessageType.Question);
            var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

            // Aktuelles Hauptfenster als Parent setzen
            await messageDialog.ShowDialog(mainWindow!);

            if (messageDialog.Result == MessageResult.Yes)
            {
                using var httpClient = new HttpClient { BaseAddress = new Uri(API_BASE_URL) };


                var response = await httpClient.DeleteAsync($"MockVaultEntries/{entity.Guid}");

                if (response.IsSuccessStatusCode == false)
                {
                    messageDialog = new MessageDialog("Error", "Beim Löschen ist ein Fehler aufgetreten!", MessageType.Error);
                    await messageDialog.ShowDialog(mainWindow!);
                }
                else
                {
                    _ = LoadEntitiesAsync();
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

                Entities.Clear();
                foreach (var entity in _entities)
                {
                    if (entity.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase))
                    {
                        Entities.Add(entity);
                    }
                }
                if (selectedItem != null)
                {
                    SelectedItem = Entities.FirstOrDefault(e => e.Guid == selectedItem.Guid);
                }
            });
        }
        private async Task LoadEntitiesAsync()
        {
            try
            {
                using var httpClient = new HttpClient { BaseAddress = new Uri(API_BASE_URL) };
                var response = await httpClient.GetStringAsync("MockVaultEntries");
                var entities = JsonSerializer.Deserialize<List<Models.MockVaultEntry>>(response, _jsonSerializerOptions);

                if (entities != null)
                {
                    _entities.Clear();
                    foreach (var entity in entities)
                    {
                        _entities.Add(entity);
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