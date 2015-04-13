var SuecaCard;
(function (SuecaCard) {
    (function (GameState) {
        GameState[GameState["Player1Turn"] = 0] = "Player1Turn";
        GameState[GameState["Player2Turn"] = 1] = "Player2Turn";
        GameState[GameState["SelfTurn"] = 2] = "SelfTurn";
        GameState[GameState["Player3Turn"] = 3] = "Player3Turn";
    })(SuecaCard.GameState || (SuecaCard.GameState = {}));
    var GameState = SuecaCard.GameState;
    ;
})(SuecaCard || (SuecaCard = {}));
//# sourceMappingURL=GameEnum.js.map