///<reference path="GameEnum.ts" />
///<reference path="CardDefinition.ts"/>
///<reference path="PlayerDefinition.ts"/>

module SuecaCard {
    export class GameInfo {
        gameState : GameState;
        listCard: Card[];
        gameNumber : number;

        constructor(gameNumber: number, ownToken : string) {
            this.gameNumber = gameNumber;
            //this.listPlayer = new Array();
            
        }

        setState(state: GameState) {
            this.gameState = state;
        }
        
        set

        


    }

}