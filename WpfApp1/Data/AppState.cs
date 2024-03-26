using WpfApp1.ViewModels;

namespace WpfApp1.Data
{
    public class AppStateVM : ViewModelBase
    {
        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set => SetProperty(ref _selectedStudent, value);
        }

        public void ClearState()
        {
            SelectedStudent = null;
        }
    }
}
