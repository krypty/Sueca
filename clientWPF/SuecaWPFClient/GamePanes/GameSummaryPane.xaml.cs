using System.Windows;

namespace suecaWPFClient.GamePanes
{
    /// <summary>
    /// Interaction logic for GameSummaryPane.xaml
    /// </summary>
    public partial class GameSummaryPane : GameStatePaneA
    {
        public GameSummaryPane()
        {
            InitializeComponent();
        }

        protected override void Quit()
        {
            throw new System.NotImplementedException();
        }

        private void BtnQuit_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeState(GameState.ListRoom);
        }
    }
}
