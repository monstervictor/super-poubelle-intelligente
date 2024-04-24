using SuperPoubelle.BalanceCommunication;
using System.Windows.Input;

namespace SuperPoubelle.ViewModels
{
    public class GarbageIdentificationViewModel : ViewModelBase
    {
        private bool _isMonitoring;

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
            _isMonitoring = false;
            AppState.SelectedBin = selection;
            NextCommand.Execute(null);
        }

        internal void BeginMonitoringSelection()
        {
            if (_isMonitoring) return;
            _isMonitoring = true;
            Task.Run(() => FetchBalances());
        }

        private Stack<IEnumerable<BalanceReport>> _data = new Stack<IEnumerable<BalanceReport>>();

        private async Task FetchBalances()
        {
            _isMonitoring = true;
            do
            {
                var balances = await BalanceClient.Instance.GetBalanceReports();
                if (!_data.TryPeek(out var topOfStack))
                {
                    _data.Push(balances);
                    continue;
                }

                // Magic
                //PerformSelectBin(BinSelection.Compost);

                _data.Push(balances);
                await Task.Delay(500);

            } while (_isMonitoring);
            _data.Clear();
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
