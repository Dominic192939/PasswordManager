using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PasswordManager.MVVMApp.Views
{
    public partial class VaultEntryView : UserControl
    {
        public VaultEntryView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackText.Text = "Daten erfolgreich gespeichert!";
            FeedbackText.IsVisible = !FeedbackText.IsVisible;
        }
    }
}