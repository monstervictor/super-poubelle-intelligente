using System.Collections.ObjectModel;
using SuperPoubelle.Data;

namespace SuperPoubelle.ViewModels
{
    public class AppStateVM : ViewModelBase
    {
        public AppStateVM()
        {
            InitializeScores();
            Solutions = new Dictionary<ItemOptionEnum, BinSelection>
            {
                { ItemOptionEnum.Vegetable, BinSelection.Compost},
                { ItemOptionEnum.Fruit, BinSelection.Compost},
            };
        }

        private void InitializeScores()
        {
            var scores = StudentFileReader.Instance.Scores.Select(score => new StudentScore
            {
                Student = StudentFileReader.Instance.CodeToStudent[score.Key],
                Score = score.Value,
            });
            Scores = new ObservableCollection<StudentScore>(scores);
        }

        private Student _selectedStudent;
        private ObservableCollection<StudentScore> _scores;
        private ItemOptionEnum _garbageSource = ItemOptionEnum.Unknown;
        private BinSelection _selectedBin = BinSelection.None;

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set => SetProperty(ref _selectedStudent, value);
        }

        public void ClearState()
        {
            SelectedStudent = null!;
            GarbageSource = ItemOptionEnum.Unknown;
            SelectedBin = BinSelection.None;
        }

        internal void AddPoint()
        {
            var targetScore = Scores.SingleOrDefault(x => x.Student == SelectedStudent);
            if (targetScore == null)
            {
                targetScore = new StudentScore { Student = SelectedStudent, Score = 0 };
                Scores.Add(targetScore);
            }
            targetScore.Score++;
        }

        public Dictionary<ItemOptionEnum, BinSelection> Solutions { get; }

        public ObservableCollection<StudentScore> Scores { get => _scores; set => SetProperty(ref _scores, value); }
        public ItemOptionEnum GarbageSource { get => _garbageSource; set => SetProperty(ref _garbageSource, value); }
        public BinSelection SelectedBin { get => _selectedBin; set => SetProperty(ref _selectedBin, value); }
    }

    public class StudentScore : ViewModelBase
    {
        public Student Student { get; init; }
        private int _score;
        public int Score { get => _score; set => SetProperty(ref _score, value); }
    }

    public class StudentScoreEventArgs : EventArgs
    {
        public StudentScore StudentScore { get; init; }
    }
}
