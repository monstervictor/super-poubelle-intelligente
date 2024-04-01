﻿
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class RewardViewModel : ViewModelBase
    {
        public RewardViewModel(AppStateVM appState, MainWindowViewModel mainVm)
        {
            AppState = appState;
            this.mainVm = mainVm;
            CorrectBin = BinSelection.Recycling;
        }

        private AppStateVM appState;
        public AppStateVM AppState { get => appState; set => SetProperty(ref appState, value); }
        public BinSelection CorrectBin { get => _correctBin; set => SetProperty(ref _correctBin, value); }

        private bool isSuccessful;
        private BinSelection _correctBin;

        public bool IsSuccessful { get => isSuccessful; set => SetProperty(ref isSuccessful, value); }

        internal void Initialize()
        {
            CorrectBin = appState.Solutions[AppState.GarbageSource];
            isSuccessful = CorrectBin == AppState.SelectedBin;
        }

        private RelayCommand restartCommand;
        private readonly MainWindowViewModel mainVm;

        public ICommand RestartCommand => restartCommand ??= new RelayCommand(Restart);

        private void Restart()
        {
            mainVm.Restart();
        }
    }
}
