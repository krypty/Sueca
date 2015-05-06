using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace suecaWPFClient.GamePanes
{
    internal class PaneSwitcher
    {
        // static
        private static readonly Dictionary<GameState, GameStatePaneA> DictUserControls = new Dictionary<GameState, GameStatePaneA>();

        // members
        private readonly ContentControl _control;
        public delegate void BoardEnabledEventHandler(bool isEnabled);
        public event BoardEnabledEventHandler BoardEnabled;

        public PaneSwitcher(ContentControl control)
        {
            _control = control;

            PopulatePanes();
            AddEventHandlers();

            StateChanged(GameState.ListRoom);
        }

        private void AddEventHandlers()
        {
            foreach (var entry in DictUserControls)
            {
                entry.Value.OnStateChanged += StateChanged;
            }
        }

        private void StateChanged(GameState state)
        {
            if (BoardEnabled != null)
            {
                bool isBoardEnable = state.Equals(GameState.InGame);
                BoardEnabled(isBoardEnable);
            }
            _control.Content = DictUserControls[state];
        }

        private static void PopulatePanes()
        {
            DictUserControls.Add(GameState.ListRoom, new RoomPane());
            DictUserControls.Add(GameState.WaitingRoom, new RoomSummaryPane());
            DictUserControls.Add(GameState.EndGame, new GameEndPane());
            DictUserControls.Add(GameState.InGame, new InGamePane());
        }
    }
}
