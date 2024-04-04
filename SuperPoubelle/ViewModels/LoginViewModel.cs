using SuperPoubelle.Data;

namespace SuperPoubelle.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(AppStateVM appState)
        {
            AppState = appState;
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
                        AppState.SelectedStudent = null;
                    }
                    else if (StudentFileReader.Instance.CodeToStudent.TryGetValue(value, out var student))
                    {
                        ShowGreenCheck = System.Windows.Visibility.Visible;
                        ShowCross = System.Windows.Visibility.Hidden;
                        AppState.SelectedStudent = student;
                    }
                    else
                    {
                        ShowGreenCheck = System.Windows.Visibility.Hidden;
                        ShowCross = System.Windows.Visibility.Visible;
                        AppState.SelectedStudent = null;
                    }
                }
            }
        }

        private System.Windows.Visibility showGreenCheck = System.Windows.Visibility.Hidden;

        public System.Windows.Visibility ShowGreenCheck { get => showGreenCheck; set => SetProperty(ref showGreenCheck, value); }

        private System.Windows.Visibility showCross = System.Windows.Visibility.Hidden;

        public System.Windows.Visibility ShowCross { get => showCross; set => SetProperty(ref showCross, value); }
        public AppStateVM AppState { get; }

        public override bool CanExecuteNextCommand()
        {
            return AppState.SelectedStudent != null;
        }
        public override void Clear()
        {
            StudentNumber = string.Empty;
        }
    }
}
