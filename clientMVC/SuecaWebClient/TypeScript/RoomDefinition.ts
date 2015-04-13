///<reference path="RoomEnums.ts" />
///<reference path="PlayerDefinition.ts"/>


module SuecaCard {
    export class Room {
        roomId: string;
        playerList: Player[] = new Array(4);
        gameHistory: GameInfo[];
        currentGame: GameInfo;
        roomState: RoomState;

        constructor(selfToken: string, roomId: string) {
            this.playerList[0] = new Player(selfToken, "El Gary Puto");
            this.roomId = roomId;
        }

        loadImages() {
            
        }


        setPlayer(token: string, position: number) {
            var newPlayer = new Player(token, "Ennemi wesh");
            this.playerList[position] = newPlayer;
        }




        getPlayerFromToken(playerToken: string) {
            for (var p in this.playerList) {
                if (this.playerList.hasOwnProperty(p)) {
                    if (p.playerToken === playerToken) {
                        return p.playerToken;
                    }


                }
            }
            return null;

        }

        drawScene() {
            


        }


        drawCards() {
            

        }


    }

}