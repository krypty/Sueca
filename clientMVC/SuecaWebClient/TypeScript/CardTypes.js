var SuecaCards;
(function (SuecaCards) {
    (function (CardColor) {
        CardColor[CardColor["Diamond"] = 0] = "Diamond";
        CardColor[CardColor["Heart"] = 1] = "Heart";
        CardColor[CardColor["Spades"] = 2] = "Spades";
        CardColor[CardColor["Clubs"] = 3] = "Clubs";
    })(SuecaCards.CardColor || (SuecaCards.CardColor = {}));
    var CardColor = SuecaCards.CardColor;
    ;
    (function (CardValue) {
        CardValue[CardValue["Two"] = 0] = "Two";
        CardValue[CardValue["Three"] = 1] = "Three";
        CardValue[CardValue["Four"] = 2] = "Four";
        CardValue[CardValue["Five"] = 3] = "Five";
        CardValue[CardValue["Seven"] = 4] = "Seven";
        CardValue[CardValue["Jack"] = 5] = "Jack";
        CardValue[CardValue["Queen"] = 6] = "Queen";
        CardValue[CardValue["King"] = 7] = "King";
        CardValue[CardValue["Ace"] = 8] = "Ace";
    })(SuecaCards.CardValue || (SuecaCards.CardValue = {}));
    var CardValue = SuecaCards.CardValue;
    ;
})(SuecaCards || (SuecaCards = {}));
//# sourceMappingURL=CardTypes.js.map