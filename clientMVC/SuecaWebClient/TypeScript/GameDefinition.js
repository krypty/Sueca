///<reference path="GameEnum.ts" />
///<reference path="CardDefinition.ts"/>
///<reference path="PlayerDefinition.ts"/>
var SuecaCard;
(function (SuecaCard) {
    var GameInfo = (function () {
        function GameInfo(gameNumber, ownToken) {
            this.gameNumber = gameNumber;
            //this.listPlayer = new Array();
        }
        GameInfo.prototype.setState = function (state) {
            this.gameState = state;
        };
        return GameInfo;
    })();
    SuecaCard.GameInfo = GameInfo;
})(SuecaCard || (SuecaCard = {}));
//# sourceMappingURL=GameDefinition.js.map