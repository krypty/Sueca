///<reference path="CardEnums.ts" />

module SuecaCard {
    export class Card {
        color: CardColor;
        value: CardValue;
        image: string;

        constructor(color: CardColor, value:CardValue, image:string) {
            this.color = color;
            this.value = value;
            this.image = image;

        }

    }


}