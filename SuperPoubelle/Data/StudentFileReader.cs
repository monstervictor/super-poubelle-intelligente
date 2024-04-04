using System.IO;
using System.Text;
using SuperPoubelle.ViewModels;

namespace SuperPoubelle.Data
{
    public class StudentFileReader
    {
        private static StudentFileReader? _instance;

        public static StudentFileReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StudentFileReader();
                return _instance;
            }
        }

        public Dictionary<string, Student> CodeToStudent { get; }

        public IReadOnlyDictionary<string, int> Scores => _scores;
        private Dictionary<string, int> _scores;

        private StudentFileReader()
        {
            CodeToStudent = ReadStudentsFile();
            _scores = ReadGlobalScoreFile();
        }

        private Dictionary<string, int> ReadGlobalScoreFile(string fileName = "global_scores.csv")
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(path, fileName);
            if (!File.Exists(file))
            {
                return [];
            }
            var results = new Dictionary<string, int>();
            var rawDataCollection = File.ReadAllLines(file).Skip(1);
            foreach (var rawData in rawDataCollection)
            {
                var values = rawData.Split(',');
                results.Add(values[0], int.Parse(values[1]));
            }
            return results;
        }

        public void AddPoint(string studentCode)
        {
            if (!_scores.TryGetValue(studentCode, out var score))
                score = 0;
            _scores[studentCode] = ++score;

            WriteScoreFile(Scores);
            WriteDifferentialScoreFile(studentCode);
        }

        private void WriteDifferentialScoreFile(string studentCode)
        {
            var differentialState = ReadGlobalScoreFile("tokens_to_give.csv");
            if (!differentialState.TryGetValue(studentCode, out var score))
                score = 0;
            differentialState[studentCode] = ++score;
            WriteScoreFile(differentialState, "tokens_to_give.csv");
        }

        private void WriteScoreFile(IReadOnlyDictionary<string, int> toWrite, string fileName = "global_scores.csv")
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(path, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Fiche,Score");
            foreach ((var id, var score) in toWrite)
            {
                sb.AppendLine($"{id},{score}");
            }
            File.WriteAllText(file, sb.ToString());

        }

        private Dictionary<string, Student> ReadStudentsFile()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(path, "171-172.csv");
            var results = new Dictionary<string, Student>();
            var rawDataCollection = File.ReadAllLines(file).Skip(1);
            foreach (var rawData in rawDataCollection)
            {
                var values = rawData.Split(',');
                var student = new Student
                {
                    Code = values[0],
                    LastName = values[1],
                    FirstName = values[2],
                    Group = values[3]
                };
                results.Add(student.Code, student);
            }
            return results;
        }
    }
}
