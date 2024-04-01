using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class GarbageIdentificationViewModel : ViewModelBase
    {
        public GarbageIdentificationViewModel(AppStateVM appState)
        {
            AppState = appState;
            SelectBin = new SelectBinCommand(this);
        }

        public AppStateVM AppState { get; }
        public ICommand SelectBin { get; }

        private class SelectBinCommand : ICommand
        {
            public SelectBinCommand(GarbageIdentificationViewModel vm)
            {
                Vm = vm;
            }

            public GarbageIdentificationViewModel Vm { get; }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter) => true;

            public void Execute(object? parameter)
            {
                if (parameter is BinSelection selection)
                    Vm.PerformSelectBin(selection);
            }
        }

        public void PerformSelectBin(BinSelection selection)
        {
            AppState.SelectedBin = selection;
            NextCommand.Execute(null);
        }
    }

    public enum BinSelection
    {
        None,
        Garbage,
        Recycling,
        Compost
    }
}
