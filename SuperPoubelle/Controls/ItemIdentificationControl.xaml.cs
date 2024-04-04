using System.Windows.Controls;
using SuperPoubelle.ViewModels;

namespace SuperPoubelle.Controls
{
    /// <summary>
    /// Interaction logic for ItemIdentification.xaml
    /// </summary>
    public partial class ItemIdentificationControl : UserControl
    {
        public ItemIdentificationControl()
        {
            InitializeComponent();
        }

        public ItemIdentificationViewModel ViewModel => (ItemIdentificationViewModel)DataContext;
    }
}
