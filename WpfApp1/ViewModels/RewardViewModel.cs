namespace WpfApp1.ViewModels
{
    public class RewardViewModel : ViewModelBase
    {
        public RewardViewModel(AppStateVM appState)
        {
            AppState = appState;
            CorrectBin = BinSelection.Recycling;
            // CorrectBin = appState.Solutions[AppState.GarbageSource];
            // isSuccessful = CorrectBin == AppState.SelectedBin;
        }

        private AppStateVM appState;
        public AppStateVM AppState { get => appState; set => SetProperty(ref appState, value); }
        public BinSelection CorrectBin { get => _correctBin; set => SetProperty(ref _correctBin, value); }

        private bool isSuccessful;
        private BinSelection _correctBin;

        public bool IsSuccessful { get => isSuccessful; set => SetProperty(ref isSuccessful, value); }
    }
}
