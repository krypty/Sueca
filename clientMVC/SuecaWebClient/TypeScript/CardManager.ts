///<reference path="../Scripts/typings/jquery/jquery.d.ts"/>
///<reference path="CardDefinition.ts" /> 


module SuecaCard {
    export class CardManager {
        cardArray: Card[];

        constructor() {
            this.cardArray = new Array();
            this.addCard(new Card(CardColor.Hearts, CardValue.Two, "Images/Cards/Hearts/2.png"));
            this.addCard(new Card(CardColor.Hearts, CardValue.Three, "Images/Cards/Hearts/2.png"));

        }


        addCard(card :Card) {
            this.cardArray.push(card);
        }

        drawCard(canevas : Object){
        
        }


        getCard() {
            return this.cardArray[0];
        }

    }

          

}

var manager = new SuecaCard.CardManager();

var card = manager.getCard();

$("#myCardArea").append(manager.getCardTemplate(card));



