using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using PasswordManager.MVVMApp.Models;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.MVVMApp.Views;
using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Input;

namespace PasswordManager.MVVMApp.ViewModels
{
    public partial class MockVaultEntryViewModel : ViewModelBase
    {
        #region fields
        private MockVaultEntry _model = new();
        #endregion 

        #region properties
        public string Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }
        public Guid Guid
        {
            get => Model.Guid;
            set => Model.Guid = value;
        }
        public string Url
        {
            get => Model.Url;
            set => Model.Url = value;
        }
        public string UserName
        {
            get => Model.UserName;
            set => Model.UserName = value;
        }
        public string Email
        {
            get => Model.Email;
            set => Model.Email = value;
        }
        public string Password
        {
            get => Model.Password;
            set => Model.Password = value;
        }
        public DateTime CreatedAt
        {
            get => Model.CreatedAt;
            set => Model.CreatedAt = value;
        }

        public Action? CloseAction { get; set; }
        public MockVaultEntry Model
        {
            get => _model;
            set => _model = value ?? new();
        }
        #endregion properties

        #region command properties
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion command properties

        #region constructors
        public MockVaultEntryViewModel()
        {
            CancelCommand = new RelayCommand(() => Close());
            SaveCommand = new RelayCommand(() => Save());
        }
        #endregion constructors

        #region command methods
        private void Close()
        {
            CloseAction?.Invoke();
        }
        private async void Save()
        {
            bool canClose = false;
            using var httpClient = new HttpClient { BaseAddress = new Uri(API_BASE_URL) };
            try
            {
                if (Model.Guid == Guid.Empty)
                {
                    string json = JsonSerializer.Serialize(Model);
                    Console.WriteLine(json);

                    var endpoint = "MockVaultEntries";
                    var fullUrl = new Uri(httpClient.BaseAddress!, endpoint);
                    Console.WriteLine("Finale URL: " + fullUrl);

                    // Breakpoint hier setzen
                    var response = await httpClient.PostAsync(
                        "MockVaultEntries",
                        new StringContent((json), Encoding.UTF8, "application/json"));
                    //var response = httpClient.PostAsync(
                    //    "MockVaultEntries",
                    //    new StringContent(JsonSerializer.Serialize(Model),
                    //        Encoding.UTF8,
                    //        "application/json")).Result;

                    var endpoint2 = "MockVaultEntries";
                    var fullUrl2 = new Uri(httpClient.BaseAddress!, endpoint2);
                    Console.WriteLine("Finale URL: " + fullUrl2);



                    if (response.IsSuccessStatusCode)
                    {
                        canClose = true;
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Fehler", "Beim Speichern ist ein Fehler aufgetreten!", MessageType.Error);
                        var mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

                        await messageDialog.ShowDialog(mainWindow!);
                        Console.WriteLine($"Fehler beim Abrufen der Entities. Status: {response.StatusCode}");
                    }
                }
                else
                {
                    var response = httpClient.PutAsync($"MockVaultEntries/{Guid}", new StringContent(JsonSerializer.Serialize(Model), Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        canClose = true;
                    }
                    else
                    {
                        Console.WriteLine($"Fehler beim Abrufen der Entities. Status: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            if (canClose)
            {
                CloseAction?.Invoke();
            }
        }
        #endregion command methods
    }
}