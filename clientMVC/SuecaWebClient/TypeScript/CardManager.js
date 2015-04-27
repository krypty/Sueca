///<reference path="../Scripts/typings/jquery/jquery.d.ts"/>
///<reference path="CardDefinition.ts" /> 
var SuecaCard;
(function (SuecaCard) {
    var CardManager = (function () {
        function CardManager() {
            this.cardArray = new Array();
            this.addCard(new SuecaCard.Card(4 /* Hearts */, 1 /* Two */, "/Images/Cards/Hearts/2.png"));
            this.addCard(new SuecaCard.Card(4 /* Hearts */, 2 /* Three */, "/Images/Cards/Hearts/2.png"));
        }
        CardManager.prototype.addCard = function (card) {
            this.cardArray.push(card);
        };
        CardManager.prototype.drawCard = function (canevas) {
        };
        CardManager.prototype.getCard = function () {
            return this.cardArray[0];
        };
        return CardManager;
    })();
    SuecaCard.CardManager = CardManager;
})(SuecaCard || (SuecaCard = {}));
var manager = new SuecaCard.CardManager();
var card = manager.getCard();
//$("#myCardArea").append(manager.getCardTemplate(card));
//# sourceMappingURL=CardManager.js.map