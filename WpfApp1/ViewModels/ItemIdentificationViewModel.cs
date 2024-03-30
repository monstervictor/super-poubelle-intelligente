using System.ComponentModel;

namespace WpfApp1.ViewModels
{
    public class ItemIdentificationViewModel : ViewModelBase
    {
        public ItemIdentificationViewModel(AppStateVM appState)
        {
            AppState = appState;
            ItemOptions = new List<ItemOption>
            {
                new() { Name = "Test" }
            };
        }
        public AppStateVM AppState { get; set; }

        public List<ItemOption> ItemOptions { get; set; }

    }

    public class ItemOption
    {
        public string Name { get; set; } = string.Empty;
    }
}
