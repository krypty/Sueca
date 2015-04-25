using System;
using System.ServiceModel;

namespace SuecaContracts
{
    public interface ISuecaCallbackContract
    {
        // This method will be invoked every time a server action is made. Clients will read gameInfo and will update their game board with it.
        //[OperationContract(IsOneWay = true)]
        //void gameInfoUpdated(IGameInfo gameInfo);


        // TODO: remove it
        [OperationContract(IsOneWay = true)]
        void GameStarted(String message);


        [OperationContract(IsOneWay = true)]
        void RoomUpdated(Room room);
    }
}
