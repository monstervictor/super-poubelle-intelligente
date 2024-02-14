using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel;

        public MainWindowViewModel()
        {
            ViewModels =
            [
                new IdentificationViewModel(),
                new ItemIdentificationViewModel(),
                new GarbageIdentificationViewModel(),
                new RewardViewModel(),
            ];            
            _nextCommand = new RelayCommand(() =>
            {
                var current = Array.IndexOf(ViewModels, SelectedViewModel);
                SelectedViewModel = ViewModels[current + 1];
            }, () =>
            {
                var current = Array.IndexOf(ViewModels, SelectedViewModel);
                return current < ViewModels.Length - 1;
            });
            _previousCommand = new RelayCommand(() =>
            {
                var current = Array.IndexOf(ViewModels, SelectedViewModel);
                SelectedViewModel = ViewModels[current - 1];
            }, () =>
            {
                var current = Array.IndexOf(ViewModels, SelectedViewModel);
                return current != 0;
            });

            SelectedViewModel = ViewModels.First();
        }

        public ViewModelBase[] ViewModels { get; }

        public ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel; 
            set
            {
                if (_selectedViewModel != value)
                {
                    _selectedViewModel = value;
                    OnNotifyPropertyChanged(nameof(SelectedViewModel));
                    _nextCommand.NotifyCanExecuteChanged();
                    _previousCommand.NotifyCanExecuteChanged();
                }
            }
        }

        private RelayCommand _nextCommand;
        public ICommand NextCommand => _nextCommand;
        private RelayCommand _previousCommand;
        public ICommand PreviousCommand => _previousCommand;
    }
}
