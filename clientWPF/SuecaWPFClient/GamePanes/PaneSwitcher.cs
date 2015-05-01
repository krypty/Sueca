﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace suecaWPFClient
{
    internal class PaneSwitcher
    {
        private ContentControl _control;

        private readonly Dictionary<GameState, GameStatePaneA> _dictUserControls =
            new Dictionary<GameState, GameStatePaneA>();

        public PaneSwitcher(ContentControl control)
        {
            _control = control;

            PopulatePanes();
            AddEventHandlers();

            StateChanged(GameState.ListRoom);
        }

        // TODO: remove me !
        [Obsolete]
        public void IncrementPane()
        {
            StateChanged(GameState.EndGame);
        }

        private void AddEventHandlers()
        {
            foreach (var entry in _dictUserControls)
            {
                entry.Value.OnStateChanged += StateChanged;
            }
        }

        private void StateChanged(GameState state)
        {
            Console.WriteLine("state changed " + state.ToString());
            _control.Content = _dictUserControls[state];
        }

        private void PopulatePanes()
        {
            _dictUserControls.Add(GameState.ListRoom, new RoomPane());
            _dictUserControls.Add(GameState.WaitingRoom, new RoomSummaryPane());
            _dictUserControls.Add(GameState.EndGame, new GameEndPane());
            _dictUserControls.Add(GameState.InGame, new GameSummaryPane());
        }
    }
}