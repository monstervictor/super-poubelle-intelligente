using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public class AppStateVM : ViewModelBase
    {
        public AppStateVM()
        {
            Scores =
            [
                new StudentScore
                {
                    Student = new Student { Code = "1", FirstName = "Benjamin Alberto", LastName = "Jimenez Amorim", Group = "1" },
                    Score = 5
                }
            ];
        }

        private Student _selectedStudent;
        private ObservableCollection<StudentScore> _scores;

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set => SetProperty(ref _selectedStudent, value);
        }

        public void ClearState()
        {
            SelectedStudent = null;
        }

        public ObservableCollection<StudentScore> Scores { get => _scores; set => SetProperty(ref _scores, value); }
    }

    public class StudentScore : ViewModelBase
    {
        public Student Student { get; init; }
        private int _score;
        public int Score { get => _score; set => SetProperty(ref _score, value); }
    }
}
