///<reference path="CardEnums.ts" />
var SuecaCard;
(function (SuecaCard) {
    var Card = (function () {
        function Card(color, value, image) {
            this.color = color;
            this.value = value;
            this.image = image;
        }
        return Card;
    })();
    SuecaCard.Card = Card;
})(SuecaCard || (SuecaCard = {}));
//# sourceMappingURL=CardDefinition.js.map