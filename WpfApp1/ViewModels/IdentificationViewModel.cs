using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using WpfApp1.Data;

namespace WpfApp1.ViewModels
{
    public class IdentificationViewModel : ViewModelBase
    {

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                OnNotifyPropertyChanged(propertyName);                
                return true;
            }

            return false;
        }

        private string studentNumber = string.Empty;

        public string StudentNumber
        {
            get => studentNumber; set
            {
                if (SetProperty(ref studentNumber, value))
                {
                    if (value == string.Empty)
                    {
                        ShowGreenCheck = System.Windows.Visibility.Hidden;
                        ShowCross = System.Windows.Visibility.Hidden;
                        SelectedStudent = null;
                    }
                    else if (StudentFileReader.Instance.CodeToStudent.TryGetValue(value, out var student))
                    {
                        ShowGreenCheck = System.Windows.Visibility.Visible;
                        ShowCross = System.Windows.Visibility.Hidden;
                        SelectedStudent = student;
                    }
                    else
                    {
                        ShowGreenCheck = System.Windows.Visibility.Hidden;
                        ShowCross = System.Windows.Visibility.Visible;
                        SelectedStudent = null;
                    }
                }
            }
        }

        public Student? SelectedStudent { get; set; }

        private System.Windows.Visibility showGreenCheck = System.Windows.Visibility.Hidden;

        public System.Windows.Visibility ShowGreenCheck { get => showGreenCheck; set => SetProperty(ref showGreenCheck, value); }

        private System.Windows.Visibility showCross = System.Windows.Visibility.Hidden;

        public System.Windows.Visibility ShowCross { get => showCross; set => SetProperty(ref showCross, value); }

    }
}
