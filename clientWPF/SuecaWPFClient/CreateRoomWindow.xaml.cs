using System.Windows;

namespace suecaWPFClient
{
    /// <summary>
    /// Interaction logic for CreateRoomWindow.xaml
    /// </summary>
    public partial class CreateRoomWindow : Window
    {
        public CreateRoomWindow(string title)
        {
            InitializeComponent();
            PasswordTextBox.Focus();
            Title = title;
        }

        public string RoomPassword
        {
            get { return PasswordTextBox.Password; }
            set { PasswordTextBox.Password = value; }
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
