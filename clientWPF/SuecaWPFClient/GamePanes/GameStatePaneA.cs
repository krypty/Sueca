using System;
using System.Windows.Controls;

namespace suecaWPFClient.GamePanes
{
    public enum GameState
    {
        ListRoom,
        WaitingRoom,
        InGame,
        EndGame
    };

    public abstract class GameStatePaneA : UserControl
    {
        public delegate void StateChangedEventHandler(GameState state);
        public event StateChangedEventHandler OnStateChanged;

        protected GameStatePaneA()
        {

        }

        protected abstract void Quit();

        protected void ChangeState(GameState state)
        {
            if (OnStateChanged != null)
            {
                OnStateChanged(state);
                //TODO: if GameState.ListRoom --> Remove content on ServiceManager ?
            }
        }

    }
}
