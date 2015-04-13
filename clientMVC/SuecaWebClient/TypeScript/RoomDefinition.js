///<reference path="RoomEnums.ts" />
///<reference path="PlayerDefinition.ts"/>
var SuecaCard;
(function (SuecaCard) {
    var Room = (function () {
        function Room(selfToken, roomId) {
            this.playerList = new Array(4);
            this.playerList[0] = new SuecaCard.Player(selfToken, "El Gary Puto");
            this.roomId = roomId;
        }
        Room.prototype.loadImages = function () {
        };
        Room.prototype.setPlayer = function (token, position) {
            var newPlayer = new SuecaCard.Player(token, "Ennemi wesh");
            this.playerList[position] = newPlayer;
        };
        Room.prototype.getPlayerFromToken = function (playerToken) {
            for (var p in this.playerList) {
                if (this.playerList.hasOwnProperty(p)) {
                    if (p.playerToken === playerToken) {
                        return p.playerToken;
                    }
                }
            }
            return null;
        };
        Room.prototype.drawScene = function () {
        };
        Room.prototype.drawCards = function () {
        };
        return Room;
    })();
    SuecaCard.Room = Room;
})(SuecaCard || (SuecaCard = {}));
//# sourceMappingURL=RoomDefinition.js.map