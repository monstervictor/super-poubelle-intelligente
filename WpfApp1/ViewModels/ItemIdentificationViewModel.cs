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
                new() { Name = ItemOptionEnum.Meat },
                new() { Name = ItemOptionEnum.Snacks },
                new() { Name = ItemOptionEnum.SnackEnclosure },
                new() { Name = ItemOptionEnum.Recycling1 },
                new() { Name = ItemOptionEnum.Recycling2 },
                new() { Name = ItemOptionEnum.Recycling3 },
                new() { Name = ItemOptionEnum.Recycling4,},
                new() { Name = ItemOptionEnum.Recycling5 },
                new() { Name = ItemOptionEnum.Recycling6 },
                new() { Name = ItemOptionEnum.Recycling7 },
            };
            SelectGarbageCommand = new CommitToItemCommand(this);
        }

        public ICommand SelectGarbageCommand { get; }

        private void ItemSelected(ItemOptionEnum item)
        {
            AppState.GarbageSource = item;
            NextCommand.Execute(item);
        }

        public AppStateVM AppState { get; }

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
        Recycling1,
        Recycling2,
        Recycling3,
        Recycling4,
        Recycling5,
        Recycling6,
        Recycling7,
        Meat,
        Snacks,
        SnackEnclosure,
    }
}
