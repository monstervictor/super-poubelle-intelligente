﻿namespace WpfApp1.ViewModels
{
    public class RewardViewModel : ViewModelBase
    {
        public RewardViewModel(AppStateVM appState)
        {
            AppState = appState;
        }

        private AppStateVM appState;
        public AppStateVM AppState { get => appState; set => SetProperty(ref appState, value); }
    }
}
