using System.Windows.Input;

namespace SuperPoubelle.ViewModels
{
    public class ItemIdentificationViewModel : ViewModelBase
    {
        public ItemIdentificationViewModel(AppStateVM appState)
        {
            AppState = appState;
            ItemOptions = new List<ItemOption>
            {
                new() { Name = ItemOptionEnum.Fruit ,
                Description = "Fruits"},
                new() { Name = ItemOptionEnum.Vegetable ,
                Description = "Légumes"},
                new() { Name = ItemOptionEnum.Meat ,
                Description = "Viande"},
                new() { Name = ItemOptionEnum.Snacks ,
                Description = "Collations"},
                new() { Name = ItemOptionEnum.SnackEnclosure,
                Description = "Sachet de chips"},
                new() { Name = ItemOptionEnum.Recycling1 ,
                Description = "Plastique 1"},
                new() { Name = ItemOptionEnum.Recycling2 ,
                Description = "Plastique 2"},
                new() { Name = ItemOptionEnum.Recycling3 ,
                Description = "Plastique 3"},
                new() { Name = ItemOptionEnum.Recycling4,
                Description = "Plastique 4"},
                new() { Name = ItemOptionEnum.Recycling5,
                Description = "Plastique 5"},
                new() { Name = ItemOptionEnum.Recycling6,
                Description = "Plastique 6"},
                new() { Name = ItemOptionEnum.Recycling7,
                Description = "Plastique 7"},
                new() { Name = ItemOptionEnum.BrownPaper,
                Description = "Papier brun"},
                new() { Name = ItemOptionEnum.WetCardboard,
                Description = "Carton souillé"},
                new() { Name = ItemOptionEnum.Tissues,
                Description = "Tissues"},
                new() { Name = ItemOptionEnum.Alluminium,
                Description = "Alluminium"},
                new() { Name = ItemOptionEnum.Paper,
                Description = "Papier"},
                new() { Name = ItemOptionEnum.Cardboard,
                Description = "Carton"},
                new() { Name = ItemOptionEnum.Glass,
                Description = "Verre"},
                new() { Name = ItemOptionEnum.NonRecyclable,
                Description = "Non recyclable"},
                new() { Name = ItemOptionEnum.Unknown,
                Description = "Inconnu"},
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
        public string Description { get; set; }
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
        BrownPaper,
        WetCardboard,
        Tissues,
        Alluminium,
        Paper,
        Cardboard,
        Glass,
        NonRecyclable,
    }
}
