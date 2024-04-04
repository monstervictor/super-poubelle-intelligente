
using CommunityToolkit.Mvvm.Input;
using SuperPoubelle;
using System.Windows.Input;
using SuperPoubelle.Data;

namespace SuperPoubelle.ViewModels
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

        internal void CommitResult()
        {
            CorrectBin = appState.Solutions[AppState.GarbageSource];
            IsSuccessful = CorrectBin == AppState.SelectedBin;
            if (isSuccessful)
            {
                Task.Run(() => StudentFileReader.Instance.AddPoint(AppState.SelectedStudent.Code));
                AppState.AddPoint();
            }
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
