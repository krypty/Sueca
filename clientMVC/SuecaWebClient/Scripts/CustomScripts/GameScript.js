
/**
 * Card list
 */


cardsColor = {};

var colors = ["Hearts", "Spades", "Diamonds", "Clubs"];


for (var i = 0; i < 4; i++) {
    cardsForColor = {};
    cardsForColor["As"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/As.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "As";
    cardsForColor["2"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/2.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "2";
    cardsForColor["3"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/3.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "3";
    cardsForColor["4"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/4.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "4";
    cardsForColor["5"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/5.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "5";
    cardsForColor["6"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/6.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "6";
    cardsForColor["7"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/7.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "7";
    cardsForColor["J"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/J.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "J";
    cardsForColor["Q"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/Q.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "Q";
    cardsForColor["K"] = new createjs.Bitmap("/Images/Cards/" + colors[i] + "/K.png");
    cardsForColor["As"].CardColor = colors[i];
    cardsForColor["As"].CardValue = "K";
    cardsColor[colors[i]] = cardsForColor;
    
}




console.log("CARDS :" + cardsColor);
//var roomId = "a092nsd";
//var playerToken = "sdaSDJ";




var gameState = null;

var stage, output;
//enum 

playerHandScalling = 1;
playerBoardScaling = 1;
playerHandleScaling = 1;
otherPlayersScale = 1;


var canPlay = true;
var tempPosition = 3;
var numberCard = 9;


var playerSpaces = [];
playerSpaces[0] = { x: 0.5, y: 0.55 };
playerSpaces[1] = { x: 0.2, y: 0.5 };
playerSpaces[2] = { x: 0.5, y: 0.3 };
playerSpaces[3] = { x: 0.8, y: 0.5 };

var myArea = new Object();
myArea.x = 0.2;
myArea.y = 0.85;
myArea.width = 0.6;
myArea.height = 0.2;

var boardArea = new Object();
boardArea.x = 0.2;
boardArea.y = 0.2;
boardArea.width = 0.60;
boardArea.height = 0.60;

var player1Area = new Object();
player1Area.x = 0.05;
player1Area.y = 0.2;
player1Area.width = 0;
player1Area.height = 0.6;
player1Area.rotation = 90;



var player2Area = new Object();
player2Area.x = 0.2;
player2Area.y = -0.1;
player2Area.width = 0.6;
player2Area.height = 0;
player2Area.rotation = 180;

var player3Area = new Object();
player3Area.x = 1 - player1Area.x;
player3Area.y = 0.2;
player3Area.width = 0;
player3Area.height = 0.6;
player3Area.rotation = -90;




var backCard = new createjs.Bitmap("/Images/Cards/Back.png");
var gameBoard = new createjs.Bitmap("/Images/GameBoard.png");
var playersCard = [];

function clearPlayersCards() {

    while (playersCard.length > 0) {
        var c = playersCard.pop();
        stage.removeChild(c);

    }
}
function getPlayerOriginalAndPositions(playerArea) {
    var card = backCard.clone();
    var playerOx = playerArea.x * stage.canvas.width;
    var playerOy = playerArea.y * stage.canvas.height;
    var playerOffsetX = playerArea.width * stage.canvas.width / 10;
    var playerOffsetY = playerArea.height * stage.canvas.height / 10;
    return { card: card, ox: playerOx, oy: playerOy, offsetX: playerOffsetX, offsetY: playerOffsetY };



}
function newScaleHandPlayers(Card) {
    
}

var player1Cards = 10;
var player2Cards = 10;
var player3Cards = 10;

var _gameState = 1;
var _playerPlaying = 0; //0 = Self

var _roomPlaying = 2;
var _roundColor = "Hearts";


var _roomState = new createjs.Text("La Sueca !", "10px Calibri", "#000000");
_roomState.textBaseline = "alphabetic";


var _titlePlayer1 = new createjs.Text("J1 - ", "8px Calibri", "#666666");
_titlePlayer1.textBaseline = "alphabetic";
var _titlePlayer2 = new createjs.Text("J2 - Prêt", "8px Calibri", "#666666");

_titlePlayer2.textAlign = "right";
var _titlePlayer3 = new createjs.Text("J3 - Pas prêt", "8px Calibri", "#666666");
_titlePlayer3.textBaseline = "alphabetic";

_titlePlayer3.textAlign = "right";


var playerReady = [1, 2, 0, 0];

function setPlayersState(playersState, inGame) {
    if (inGame) {
        
    }



}




function drawScene(evt) {
    /**
     *  Requires :
     *      gameState : 0 - Waiting / 1 - InGame / 2 - Score
     */
    clearPlayersCards();
    
    var widthFactor = getScalingFactor(stage.canvas.width, boardArea.width, gameBoard.image.width);
    var heightFactor = getScalingFactor(stage.canvas.height, boardArea.height, gameBoard.image.height);
    gameBoard.x = stage.canvas.width * boardArea.x;
    gameBoard.y = stage.canvas.height * boardArea.y;
    gameBoard.scaleX = widthFactor;
    gameBoard.scaleY = heightFactor;


    
    _roomState.y = stage.canvas.height - 1;
    //_roomState.scaleX = _roomState.scaleY = 0.8;

    _titlePlayer2.x = stage.canvas.width * 0.95;
    _titlePlayer3.x = stage.canvas.width;
    _titlePlayer3.y = stage.canvas.height - 1;
    switch(_gameState) {
    
        case 0:
            _roomState.text = "En attente d'autres joueurs";
            

            break;
        case 1:

            _roomState.text = "Au tour de : Joueur 1";
            drawPlayersCards();

            break;


            //cardsColor[colors[0]]["2"]
    }

    stage.update();

}





function drawPlayersCards() {
    var cardPlayer = null;
    var player1CardInfos = getPlayerOriginalAndPositions(player1Area);
    var blurFilter = new createjs.BlurFilter(20, 20, 1);
    //scaleOPlayers = stage.getBounds().height * 0.5 / player1CardInfos.card.height;
    for (var i = 0; i < player1Cards; i++) {
        cardPlayer = player1CardInfos.card.clone();
        cardPlayer.scaleX = cardPlayer.scaleY = 0.5;
        
        cardPlayer.x = player1CardInfos.ox;
        cardPlayer.y = player1CardInfos.oy + player1CardInfos.offsetY * (i + (10 - player1Cards) / 2);
        cardPlayer.rotation = player1Area.rotation;
        playersCard.push(cardPlayer);
        stage.addChild(cardPlayer);
    }

    var player2CardInfos = getPlayerOriginalAndPositions(player2Area);
    player2CardInfos.rotation = 180;
    for (var i = 0; i < player2Cards; i++) {
        cardPlayer = player2CardInfos.card.clone();
        cardPlayer.rotation = player2CardInfos.rotation;
        cardPlayer.scaleX = cardPlayer.scaleY = 0.5;
        cardPlayer.filters = [blurFilter];
        cardPlayer.x = player2CardInfos.ox + player2CardInfos.offsetX * (i + (10 - player2Cards) / 2);
        cardPlayer.y = -player2CardInfos.oy;
        
        playersCard.push(cardPlayer);
        stage.addChild(cardPlayer);
    }

    var player3CardInfos = getPlayerOriginalAndPositions(player3Area);
    
    for (var i = 0; i < player3Cards; i++) {
        cardPlayer = player3CardInfos.card.clone();
        cardPlayer.scaleX = cardPlayer.scaleY = 0.5;
        cardPlayer.x = player3CardInfos.ox;
        cardPlayer.y = player3CardInfos.oy + player3CardInfos.offsetY * (i + (10 - player3Cards) / 2);
        cardPlayer.rotation = player3Area.rotation;
        playersCard.push(cardPlayer);
        stage.addChild(cardPlayer);
    }



}
function getScalingFactor(screenSize, screenFactor, imageSize) {
    return screenFactor * screenSize / imageSize;

}

function scaleCard(card, scaling) {
    card.scaleX = scaling;
    card.scaleY = scaling;
}


function placeCards(card, area, object) {
    //switch(getCardOffset(card){}
    if (area == 0 || area == 2) {

        var xPos = stage.canvas.width * myArea.width / numberCard * tempPosition;

        card.x = stage.canvas.width * myArea.x + xPos;
        card.y = stage.canvas.height * myArea.y;
        scaleCard(card, playerHandScalling);
    }
    else {


        card.selectable = false;
        scaleCard(card, playerBoardScaling);
        card.x = stage.canvas.width * playerSpaces[0].x + object.offset.x;
        card.y = stage.canvas.height * playerSpaces[0].y + object.offset.y;
    }
    stage.update();
}


function within(xPers, yPers, areaObj) {
    return xPers > areaObj.x && xPers < areaObj.width + areaObj.x
        && yPers > areaObj.y && yPers < areaObj.height + areaObj.y;
}


function getAreaFromPosition(xPers, yPers, card) {
    if (within(xPers, yPers, myArea)) {
        return 0;
    }
    else if (within(xPers, yPers, boardArea) && canPlay) {
        return 1;
    }
    else {
        return 2;
    }
    //console.log(x + " | " + y);
}


function addHandHeldCard(Card) {
    var bitmap = new createjs.Bitmap("/Images/Cards/Clubs/As.png");
    bitmap.selectable = true;
    scaleCard(bitmap, playerHandScalling);

    bitmap.addEventListener("mousedown", function (evt) {
        if (bitmap.selectable) {
            var o = evt.target;
            o.parent.addChild(o);
            o.offset = { x: o.x - evt.stageX, y: o.y - evt.stageY };
        }
    });


    bitmap.addEventListener("pressup", function (evt) {
        if (bitmap.selectable) {
            var o = evt.target;
            /*bitmap.offsetX = o.offset.x;
            bitmap.offsetY =  o.offset.y;*/
            var area = getAreaFromPosition((evt.stageX + o.offset.x) / stage.canvas.width, (evt.stageY + o.offset.y) / stage.canvas.height, bitmap);
            placeCards(bitmap, area, o);
        }
    });


    bitmap.on("pressmove", function (evt) {
        // currentTarget will be the container that the event listener was added to:
        if (bitmap.selectable) {
            var o = evt.target;
            o.x = evt.stageX + o.offset.x;
            o.y = evt.stageY + o.offset.y;

            // make sure to redraw the stage to show the change:
            stage.update();
        }
    });
    bitmap.on("mouseover", function (evt) {
        if (bitmap.selectable) {
            scaleCard(bitmap, playerHandleScaling);

            stage.update();
        }
    });

    bitmap.on("mouseout", function (evt) {
        if (bitmap.selectable) {
            scaleCard(bitmap, playerHandScalling);
            stage.update();
        }
    });

    stage.addChild(bitmap);
    stage.update();
}

stage = null;
gameState = null;

function init() {
    
    console.log(stage);
    stage = new createjs.Stage("c");
    // this lets our drag continue to track the mouse even when it leaves the canvas:
    // play with commenting this out to see the difference.
    stage.mouseMoveOutside = true;
    stage.enableMouseOver(10);
    //var circle = new createjs.Shape();
    //circle.graphics.beginFill("red").drawCircle(0, 0, 50);
    

    

    

    stage.addChild(_titlePlayer1);
    stage.addChild(_titlePlayer2);
    stage.addChild(_titlePlayer3);
    stage.addChild(gameBoard);
    stage.addChild(_roomState);
    createjs.Ticker.addEventListener("tick", drawScene);
    
}




//var CanvasWidth;
//var CanvasHeight;

//var imageBaseWidth = 1260;
//var imageBaseHeight = 1760;

//function loadCards(context, cardObj)
//{
//    base_image = new Image();
//    base_image.src = cardObj.image;
//    base_image.onload = function () {
//        //context.drawImage(base_image, 0, 0, 100, 100 * imageBaseHeight / imageBaseWidth)
//        imageResize(base_image, imageBaseWidth, 100, document.getElementById('c'), context);

//    }
//    return;
//}



//function imageResize(img, sourceWidth, targetWidth, canvas, context)
//{

//    var steps = Math.ceil(Math.log(sourceWidth / targetWidth) / Math.log(2))

//    /// step 1 - create off-screen canvas
//    var oc = document.createElement('canvas');
//    var octx = oc.getContext('2d');

//    oc.width  = img.width  * 0.5;
//    oc.height = img.height * 0.5;

//    octx.drawImage(img, 0, 0, oc.width, oc.height);

//    //Step 2 reuses the off-screen canvas and draws the image reduced to half again:
    


//    /// step 2
//    for (var i = 1; i < steps; i++)
//    {

//        octx.drawImage(oc, 0, 0, oc.width, oc.height);
//    }

//    //And we draw once more to main canvas, again reduced to half but to the final size:

//    /// step 3
//    context.drawImage(oc, 0, 0, oc.width, oc.height, 0, 0, canvas.width,   canvas.height);

//}






//function canvasHandle()
//{
    


//}
//(function () {
//    var
//        htmlCanvas = document.getElementById('c'),

//        context = htmlCanvas.getContext('2d');

//    // Start listening to resize events and
//    // draw canvas.
//     initialize();

//    function initialize() {
//        // Register an event listener to
//        // call the resizeCanvas() function each time 
//        // the window is resized.
//        window.addEventListener('resize', resizeCanvas, false);

//        // Draw canvas border for the first time.
//        resizeCanvas();

//        loadCards(context, card);



//    }

//    function redraw()
//    {

//    }


//    function resizeCanvas() {
//        var canevas = document.getElementById('c');
//        CanvasWidth = canevas.width;
//        CanvasHeight = canevas.height;
//        redraw();
//    }

//})();




