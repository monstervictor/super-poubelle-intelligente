using System.IO;
using WpfApp1.ViewModels;

namespace WpfApp1.Data
{
    public class StudentFileReader
    {
        private static StudentFileReader _instance;

        public static StudentFileReader Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new StudentFileReader();
                return _instance;
            }
        }

        public Dictionary<string, Student> CodeToStudent { get; }

        private StudentFileReader()
        {
            string file = "G:\\VSProjects\\school garbage projet\\171-172.csv";
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
            CodeToStudent = results;
        }
    }
}
