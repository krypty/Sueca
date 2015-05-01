using System;
using System.Windows.Controls;

namespace suecaWPFClient
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
            Console.WriteLine("GameStatePaneA 1");
            if (OnStateChanged != null)
            {
                Console.WriteLine("GameStatePaneA 2");
                OnStateChanged(state);
            }
        }

    }
}
