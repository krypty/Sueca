using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuecaWebClient.SuecaService;
using SuecaWebClient.Models;


namespace SuecaWebClient.Helper
{

    public class SuecaServiceHelper
    {
        SuecaClient client = null;

        public SuecaServiceHelper()
        {
            client = new SuecaClient(new System.ServiceModel.InstanceContext(new InterfaceService()));
            
        }

        public void close()
        {
            if(client!= null)
            {
                client.Close();
            }
        }


        public static string CardColorToString(CardColor c)
        {
            switch (c)
            {
                case CardColor.Clubs:
                    return "Clubs";
                case CardColor.Diamonds:
                    return "Diamonds";
                case CardColor.Hearts:
                    return "Hearts";
                case CardColor.Spades:
                    return "Spades";
            }
            return "";
        }

        public static CardColor StringToCardColor(string color)
        {
            switch(color)
            {
                case "Clubs":
                    return CardColor.Clubs;
                case "Diamonds":
                    return CardColor.Diamonds;
                case "Hearts":
                    return CardColor.Hearts;
                case "Spades":
                    return CardColor.Spades;
            }
            return CardColor.None;
        }

        public static string CardValueToString(CardValue c)
        {
            switch (c)
            {
                case CardValue.Ace:
                    return "As";
                case CardValue.Two:
                    return "2";
                case CardValue.Three:
                    return "3";
                case CardValue.Four:
                    return "4";
                case CardValue.Five:
                    return "5";
                case CardValue.Six:
                    return "6";
                case CardValue.Seven:
                    return "7";
                case CardValue.Jack:
                    return "J";
                case CardValue.Queen:
                    return "Q";
                case CardValue.King:
                    return "K";
            }
            return "";
        }

        public static CardValue StringToCardValue(string value)
        {
            switch (value)
            {
                case "As":
                    return CardValue.Ace;
                case "2":
                    return CardValue.Two;
                case "3":
                    return CardValue.Three;
                case "4":
                    return CardValue.Four;
                case "5":
                    return CardValue.Five;
                case "6":
                    return CardValue.Six;
                case "7":
                    return CardValue.Seven;
                case "J":
                    return CardValue.Jack;
                case "Q":
                    return CardValue.Queen;
                case "K":
                    return CardValue.King;


            }
            return CardValue.None;

        }





        public bool playCard(String playerToken, string color, string value)
        {

            
            client.PlayCard(playerToken, StringToCardColor(color), StringToCardValue(value));
            return true;

        }

        public String joinRoom(string roomId, string password = "")
        {
            if (roomId != "" && client != null)
            {
                try
                {
                    string playerToken = client.JoinRoom(roomId, password, false);
                    return playerToken;
                }
                catch (Exception e)
                {
                    
                }
            }
            return "";
        }

        public GameInfoModel GetGameState(string roomId, string playerToken)
        {
            GameInfoModel gameInfos = null;
            try
            {
                gameInfos = new GameInfoModel(client.GetGameInfo(playerToken, roomId));
            }
            catch (Exception)
            {
                

                
            }
            return gameInfos;

        }



        public RoomInfoModel getRoomState(string roomId, string playerToken)
        {
            //SuecaService.Room.StateRoom
            //client.JoinRoom();
            if (roomId != "" && client != null)
            {
                try
                {


                    Room r = client.GetRoom(playerToken, roomId);
                    if (r != null)
                    {
                        int roomState = 0;
                        switch (r.RoomState)
                        {
                            case Room.StateRoom.WAITING_READY:
                                roomState = 0;
                                break;
                            case Room.StateRoom.GAME_IN_PROGRESS:
                                roomState = 1;
                                break;
                            case Room.StateRoom.END_GAME:
                                roomState = 2;
                                break;
                        }


                        return new RoomInfoModel(r.Name, playerToken, r.listPlayers, roomState);

                    }
                    else
                        return null;

                }
                catch (Exception e)
                {
                    
                }
            }
            return null;
        }

        public void sendClientReady(string playerToken, bool state)
        {
            client.SendReady(playerToken,state);
        }

        
        public Room[] getRoomsArray()
        {
            try
            {
                return client.ListRoom();
            }
            catch(Exception e)
            {
                return null;
            }
            
        }

        public String createRoom(string password = "")
        {
            try
            {
                return client.CreateRoom(password);
            }
            catch(Exception e)
            {
                return "";
            }
        }


        private class InterfaceService : SuecaService.SuecaCallback
        {

            public void GameStarted(string message)
            {
                //TODO
            }


            public void RoomUpdated(Room room)
            {
                //throw new NotImplementedException();
            }


            public void GameInfoUpdated(GameInfo gameInfo)
            {
                //throw new NotImplementedException();
            }




            public bool CheckConnection()
            {
                return true;//throw new NotImplementedException();
            }
        }


    }
}