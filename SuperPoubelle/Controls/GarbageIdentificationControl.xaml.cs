using SuperPoubelle.ViewModels;
using System.Windows.Controls;

namespace SuperPoubelle.Controls
{
    /// <summary>
    /// Interaction logic for GarbageIdentificationControl.xaml
    /// </summary>
    public partial class GarbageIdentificationControl : UserControl
    {
        public GarbageIdentificationViewModel ViewModel => (GarbageIdentificationViewModel)DataContext;

        public GarbageIdentificationControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.BeginMonitoringSelection();
        }
    }
}
