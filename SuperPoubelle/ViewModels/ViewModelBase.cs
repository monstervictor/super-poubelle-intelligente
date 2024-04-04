using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private ICommand _nextCommand;
        private ICommand _previousCommand;

        public virtual ICommand NextCommand { get => _nextCommand; set => SetProperty(ref _nextCommand, value); }
        public virtual ICommand PreviousCommand { get => _previousCommand; set => SetProperty(ref _previousCommand, value); }

        public virtual bool CanExecuteNextCommand() => true;

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;


        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                OnNotifyPropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        public virtual void Clear() { }
    }
}
