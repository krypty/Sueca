using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient.GamePanes
{
    /// <summary>
    /// Interaction logic for GameEndPane.xaml
    /// </summary>
    public partial class GameEndPane : GameStatePaneA
    {
        private List<Label> _listPlayerScore;


        public GameEndPane()
        {
            InitializeComponent();
            ServiceManager.GetInstance().OnGameInfoUpdated += OnGameInfoUpdated;

            _listPlayerScore = new List<Label>
            {
                PlayerSouthScore,
                PlayerWestScore,
                PlayerNorthScore,
                PlayerEastScore
            };
        }

        private void OnGameInfoUpdated(GameInfo gameInfo)
        {
            var listPlayer = gameInfo.ListPlayer;
            if (listPlayer.Length != 4) return;

            for (int i = 0; i < _listPlayerScore.Count; i++)
            {
                Label lbl = _listPlayerScore[i];
                Player player = listPlayer[i];

                lbl.Content = listPlayer[i].Score;
                lbl.FontWeight = listPlayer.Max(p => p.Score) == player.Score ? FontWeights.Bold : FontWeights.Normal;
            }


            PlayerSouthScore.Content = listPlayer[0].Score;
            PlayerWestScore.Content = listPlayer[1].Score;
            PlayerNorthScore.Content = listPlayer[2].Score;
            PlayerEastScore.Content = listPlayer[3].Score;
        }

        protected override void Quit()
        {
            //TODO...
        }

        private void BtnBackToRoomList_OnClick(object sender, RoutedEventArgs e)
        {
            string playerToken = ServiceManager.GetInstance().PlayerToken;
            ServiceManager.GetInstance().SendEndGameReceived(playerToken);
            ChangeState(GameState.WaitingRoom);
        }
    }
}
