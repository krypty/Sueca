using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SuecaContracts
{
    [ServiceContract(Name="Sueca", CallbackContract=typeof(ISuecaCallbackContract), SessionMode=SessionMode.Required)]
    public interface ISuecaContract
    {
        #region Join methods
        /// <summary>
        /// Create a room with an optional password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>name of the room created</returns>
        [OperationContract(IsTerminating = false, IsOneWay = false)]
        String CreateRoom(String password = "");

        /// <summary>
        /// Join a room
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="password"></param>
        /// <returns>playerToken which is unique among all players in all rooms or null if wrong password</returns>
        [OperationContract(IsTerminating = false, IsOneWay = false)]
        String JoinRoom(String roomName, String password = "", bool isUsingCallback=true);


        /// <summary>
        /// Inform the server that user is ready to start the game
        /// </summary>
        /// <param name="isReady"></param>
        [OperationContract(IsTerminating = false, IsOneWay = false)]
        void SendReady(string playerToken, bool isReady);

        [OperationContract(IsTerminating = false, IsOneWay = false)]
        List<Room> ListRoom();

        #endregion

        #region Game methods
        //TODO: add here game methods like playCard(Card card, String playerToken)


        //GameInfo GetGameInfo(String roomID, String playerToken)
        //{
        //    return this.mapRooms.get(roomID).getGameInfoClient(playerToken);
        //    return null;
        //}
        [OperationContract(IsTerminating = false, IsOneWay = false)]
        bool PlayCard(string playerToken, CardColor color, CardValue value);
        [OperationContract(IsTerminating = false, IsOneWay = false)]
        Room GetRoom(String playerToken, String roomName);
        [OperationContract(IsTerminating = false, IsOneWay = false)]
        GameInfo GetGameInfo(string playerToken, string roomId);

        #endregion
    }
}
