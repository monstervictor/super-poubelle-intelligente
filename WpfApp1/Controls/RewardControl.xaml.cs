using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Controls
{
    /// <summary>
    /// Interaction logic for RewardControl.xaml
    /// </summary>
    public partial class RewardControl : UserControl
    {
        public RewardControl()
        {
            InitializeComponent();
        }

        public RewardViewModel ViewModel => (RewardViewModel)DataContext;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CommitResult();
        }
    }
}
