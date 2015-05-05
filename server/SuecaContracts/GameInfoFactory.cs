using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuecaContracts
{
    class GameInfoFactory
    {
        public static GameInfo CreateGameInfoClient(Player player, GameInfoServer gameInfoServer)
        {
            LinkedList<Player> listPlayers = new LinkedList<Player>();
            LinkedList<Card> listCards = new LinkedList<Card>();

            //Construct list of player and cards in the order : current player first
            for (int i = 0; i < 4; i++)
            {
                listPlayers.AddLast(gameInfoServer.DictPlayers.Values.ToList().Where<Player>(p => p.NumberTurn == ((player.NumberTurn + i) % 4)).First());
                Card cardOfPlayer;

                gameInfoServer.DictCardPlayerForTurn.TryGetValue(listPlayers.ElementAt(i).Token, out cardOfPlayer);

                listCards.AddLast(cardOfPlayer);
            }

            bool isHisTurn = (gameInfoServer.NumberPlayerTurn == player.NumberTurn);
            GameInfo game = new GameInfo(player, listPlayers, listCards, isHisTurn, gameInfoServer.Asset);
            game.FirstCard = gameInfoServer.ListCardsTurn.First == null ? null : gameInfoServer.ListCardsTurn.First.Value;

            return game;
        }
    }
}
