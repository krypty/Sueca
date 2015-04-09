var SuecaCard;
(function (SuecaCard) {
    (function (CardColor) {
        CardColor[CardColor["None"] = 0] = "None";
        CardColor[CardColor["Spades"] = 1] = "Spades";
        CardColor[CardColor["Diamonds"] = 2] = "Diamonds";
        CardColor[CardColor["Clubs"] = 3] = "Clubs";
        CardColor[CardColor["Hearts"] = 4] = "Hearts";
    })(SuecaCard.CardColor || (SuecaCard.CardColor = {}));
    var CardColor = SuecaCard.CardColor;
    ;
    (function (CardValue) {
        CardValue[CardValue["None"] = 0] = "None";
        CardValue[CardValue["Two"] = 1] = "Two";
        CardValue[CardValue["Three"] = 2] = "Three";
        CardValue[CardValue["Four"] = 3] = "Four";
        CardValue[CardValue["Five"] = 4] = "Five";
        CardValue[CardValue["Six"] = 5] = "Six";
        CardValue[CardValue["Seven"] = 6] = "Seven";
        CardValue[CardValue["Jack"] = 7] = "Jack";
        CardValue[CardValue["Queen"] = 8] = "Queen";
        CardValue[CardValue["King"] = 9] = "King";
        CardValue[CardValue["Ace"] = 10] = "Ace";
    })(SuecaCard.CardValue || (SuecaCard.CardValue = {}));
    var CardValue = SuecaCard.CardValue;
    ;
})(SuecaCard || (SuecaCard = {}));
//# sourceMappingURL=CardEnums.js.map