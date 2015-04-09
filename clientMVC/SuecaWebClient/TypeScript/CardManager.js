///<reference path="../Scripts/typings/jquery/jquery.d.ts"/>
///<reference path="CardDefinition.ts" /> 
var SuecaCard;
(function (SuecaCard) {
    var CardManager = (function () {
        function CardManager() {
            this.cardArray = new Array();
            this.addCard(new SuecaCard.Card(4 /* Hearts */, 1 /* Two */, "Images/Cards/Hearts/2.png"));
        }
        CardManager.prototype.addCard = function (Card) {
            this.cardArray.push(Card);
        };
        CardManager.prototype.getCardTemplate = function (Card) {
            return "<img src='" + Card.image + "'/>";
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
$("#myCardArea").append(manager.getCardTemplate(card));
//# sourceMappingURL=CardManager.js.map