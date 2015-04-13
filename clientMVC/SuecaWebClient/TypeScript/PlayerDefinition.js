///<reference path="CardDefinition.ts" />
var SuecaCard;
(function (SuecaCard) {
    var Player = (function () {
        function Player(playerToken, name) {
            this.numOfCards = 0;
            this.playerToken = playerToken;
            this.name = name;
            this.isReady = false;
        }
        Player.prototype.setCards = function (playerCards) {
            this.listCards = playerCards;
            this.numOfCards = playerCards.length;
        };
        Player.prototype.getCards = function () {
            return this.listCards;
        };
        Player.prototype.getReady = function () {
            return this.isReady;
        };
        Player.prototype.setReady = function (isReady) {
            this.isReady = isReady;
        };
        Player.prototype.setNumOfCards = function (numOfCards) {
            this.numOfCards = numOfCards;
        };
        Player.prototype.getNumOfCards = function () {
            return this.numOfCards;
        };
        return Player;
    })();
    SuecaCard.Player = Player;
})(SuecaCard || (SuecaCard = {}));
//# sourceMappingURL=PlayerDefinition.js.map