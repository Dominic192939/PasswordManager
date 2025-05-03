//@CodeCopy
using PasswordManager.MVVMApp.ViewModels;

namespace PasswordManager.MVVMApp.ViewModels.Templates
{
    public partial class ItemViewModel : ViewModelBase
    {
        private string _name = string.Empty;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

    }
}
