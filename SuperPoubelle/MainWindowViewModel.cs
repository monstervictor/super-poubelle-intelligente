﻿using CommunityToolkit.Mvvm.Input;
using SuperPoubelle.ViewModels;
using System.Windows.Input;

namespace SuperPoubelle
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel;
        private LoginViewModel _identificationViewModel;

        public MainWindowViewModel()
        {
            ViewModels =
            [
                new LoginViewModel(AppState),
                new ItemIdentificationViewModel(AppState),
                new GarbageIdentificationViewModel(AppState),
                new RewardViewModel(AppState, this),
            ];
            InitializeCommands();
            foreach (var viewModel in ViewModels)
            {
                viewModel.NextCommand = NextCommand;
                viewModel.PreviousCommand = PreviousCommand;
            }
            Restart();
        }

        private void InitializeCommands()
        {
            _nextCommand = new RelayCommand(() =>
            {
                if (!SelectedViewModel.CanExecuteNextCommand())
                    return;
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
        }

        internal void Restart()
        {
            AppState.ClearState();
            foreach (var viewModel in ViewModels)
            {
                viewModel.Clear();
            }
            SelectedViewModel = ViewModels.First();
        }

        public AppStateVM AppState { get; set; } = new AppStateVM();

        public ViewModelBase[] ViewModels { get; }

        public ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                if (SetProperty(ref _selectedViewModel, value))
                {
                    _nextCommand.NotifyCanExecuteChanged();
                    _previousCommand.NotifyCanExecuteChanged();
                }
            }
        }

        private Student? _student;
        public Student? Student { get => _student; set => SetProperty(ref _student, value); }

        private RelayCommand _nextCommand;
        public ICommand NextCommand => _nextCommand;
        private RelayCommand _previousCommand;
        public ICommand PreviousCommand => _previousCommand;

        private System.Windows.Visibility showStudentDetails;

        public System.Windows.Visibility ShowStudentDetails { get => showStudentDetails; set => SetProperty(ref showStudentDetails, value); }
    }
}
