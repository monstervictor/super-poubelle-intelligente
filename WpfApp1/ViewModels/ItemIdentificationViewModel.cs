using WpfApp1.Data;

namespace WpfApp1.ViewModels
{
    public class ItemIdentificationViewModel : ViewModelBase
    {
        public ItemIdentificationViewModel(AppStateVM appState)
        {
            AppState = appState;
        }
        public AppStateVM AppState { get; set; }
    }
}
