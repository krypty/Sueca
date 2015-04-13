///<reference path="CardDefinition.ts" />

module SuecaCard {
    
    export class Player {
        
        playerToken : string;
        name: string;
        isReady : boolean;
        listCards: Card[];
        numOfCards : number = 0;

        constructor(playerToken: string, name: string) {
            this.playerToken = playerToken;
            this.name = name;
            this.isReady = false;
        }

        setCards(playerCards: Card[]) {
            this.listCards = playerCards;
            this.numOfCards = playerCards.length;
        }

        getCards() {
            return this.listCards;
        }

        getReady() {
            return this.isReady;
        }
        
        setReady(isReady: boolean) {
            this.isReady = isReady;
        }

        setNumOfCards(numOfCards: number) {
            this.numOfCards = numOfCards;
        }

        getNumOfCards() {
            return this.numOfCards;
        }

    }

} 