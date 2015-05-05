using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using suecaWPFClient.ServiceReference1;

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
            ServiceManager.GetInstance().OnGameInfoUpdated += OnGameInfoUpdated;
        }

        private void OnGameInfoUpdated(GameInfo gameInfo)
        {
            var listPlayer = gameInfo.ListPlayer;
            if (listPlayer.Length != 4) return;

            PlayerSouthScore.Content = listPlayer[0].Score;
            PlayerWestScore.Content = listPlayer[1].Score;
            PlayerNorthScore.Content = listPlayer[2].Score;
            PlayerEastScore.Content = listPlayer[3].Score;
        }

        protected override void Quit()
        {
            //throw new System.NotImplementedException();
        }

        private void BtnBackToRoomList_OnClick(object sender, RoutedEventArgs e)
        {
            string playerToken = ServiceManager.GetInstance().PlayerToken;
            ServiceManager.GetInstance().SendEndGameReceived(playerToken);
            ChangeState(GameState.WaitingRoom);
        }
    }
}
