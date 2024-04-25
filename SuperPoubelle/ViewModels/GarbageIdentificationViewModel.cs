using SuperPoubelle.BalanceCommunication;
using SuperPoubelle.Data;
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

        private BalanceReport[]? _previousReading;

        private async Task FetchBalances()
        {
            _isMonitoring = true;
            do
            {
                var weightReading = (await BalanceClient.Instance.GetBalanceReports()).ToArray();
                if (_previousReading == null)
                {
                    _previousReading = weightReading;
                    await Task.Delay(ConfigurationFile.Instance.Config.ReadingDelaysInMilliseconds);
                    continue;
                }

                var diff1 = _previousReading[0].Grams - weightReading[0].Grams;
                var diff2 = _previousReading[1].Grams - weightReading[1].Grams;

                if (Math.Abs(diff1) > ConfigurationFile.Instance.Config.DifferenceForGarbageDecision)
                {
                    var selection = weightReading[0].BalanceIdentifier == "0" ? BinSelection.Recycling : BinSelection.Compost;
                    _previousReading = null;
                    PerformSelectBin(selection);
                    break;
                }
                if (Math.Abs(diff2) > ConfigurationFile.Instance.Config.DifferenceForGarbageDecision)
                {
                    var selection = weightReading[1].BalanceIdentifier == "0" ? BinSelection.Recycling : BinSelection.Compost;
                    _previousReading = null;
                    PerformSelectBin(selection);
                    break;
                }

                _previousReading = weightReading;
                await Task.Delay(ConfigurationFile.Instance.Config.ReadingDelaysInMilliseconds);

            } while (_isMonitoring);
            _previousReading = null;
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
