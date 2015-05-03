using System.Windows;

namespace suecaWPFClient.GamePanes
{
    /// <summary>
    /// Interaction logic for GameEndPane.xaml
    /// </summary>
    public partial class GameEndPane : GameStatePaneA
    {
        public GameEndPane()
        {
            InitializeComponent();
        }

        protected override void Quit()
        {
            throw new System.NotImplementedException();
        }

        private void BtnBackToRoomList_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeState(GameState.ListRoom);
        }
    }
}
