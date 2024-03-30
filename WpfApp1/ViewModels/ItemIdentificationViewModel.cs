using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class ItemIdentificationViewModel : ViewModelBase
    {
        public ItemIdentificationViewModel(AppStateVM appState)
        {
            AppState = appState;
            ItemOptions = new List<ItemOption>
            {
                new() { Name = ItemOptionEnum.Fruit },
                new() { Name = ItemOptionEnum.Vegetable },
            };
            SelectGarbageCommand = new CommitToItemCommand(this);
        }

        public ICommand SelectGarbageCommand { get; }

        private void ItemSelected(ItemOptionEnum item)
        {
            AppState.GarbageSource = item;
            NextCommand.Execute(item);
        }

        public AppStateVM AppState { get; set; }

        public List<ItemOption> ItemOptions { get; set; }

        private class CommitToItemCommand : ICommand
        {
            private ItemIdentificationViewModel _vm;
            public CommitToItemCommand(ItemIdentificationViewModel vm)
            {
                _vm = vm;
            }
            public event EventHandler? CanExecuteChanged;
            public bool CanExecute(object? parameter) => true;
            public void Execute(object? parameter)
            {
                if (parameter is ItemOptionEnum item)
                    _vm.ItemSelected(item);
            }
        }
    }
    

    public class ItemOption
    {
        public ItemOptionEnum Name { get; set; }
    }

    public enum ItemOptionEnum
    {
        Unknown = 0,
        Fruit,
        Vegetable,
    }
}
