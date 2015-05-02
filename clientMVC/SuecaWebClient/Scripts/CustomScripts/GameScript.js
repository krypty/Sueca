



//var roomId = "a092nsd";
//var playerToken = "sdaSDJ";
var roomState = null;
var gameState = null;

var stage, output;
//enum 

playerHandScalling = 0.06;
playerBoardScaling = 0.08;
playerHandleScaling = 0.1;
otherPlayersScale = 0.05;


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
boardArea.x = 0.15;
boardArea.y = 0.15;
boardArea.width = 0.70;
boardArea.height = 0.70;

var player1Area = new Object();
player1Area.x = 0.05;
player1Area.y = 0.2;
player1Area.width = 0;
player1Area.height = 0.6;
player1Area.rotation = 90;



var player2Area = new Object();
player2Area.x = 0.2;
player2Area.y = 0.05;
player2Area.width = 0.6;
player2Area.height = 0;
player2Area.rotation = 180;

var player3Area = new Object();
player3Area.x = 1 - player1Area.x;
player3Area.y = 0.2;
player3Area.width = 0;
player3Area.height = 0.6;
player3Area.rotation = -90;



var player1Cards = 10;
var player2Cards = 10;
var player3Cards = 10;

var backCard = new createjs.Bitmap("/Images/Cards/Back.png");

var playersCard = [];

function updateBoard() {

    clearPlayersCards();
    drawPlayersCards();
}

function clearPlayersCards() {

    while (playersCard.length > 0) {
        var c = playersCard.pop();
        stage.removeChild(c);

    }
}
function getPlayerOriginalAndPositions(playerArea) {
    var card = backCard.clone();
    card.scaleX = otherPlayersScale;
    card.scaleY = otherPlayersScale;
    card.rotation = playerArea.rotation;

    var playerOx = playerArea.x * stage.canvas.width;
    var playerOy = playerArea.y * stage.canvas.height;
    var playerOffsetX = playerArea.width * stage.canvas.width / 10;
    var playerOffsetY = playerArea.height * stage.canvas.height / 10;
    return { card: card, ox: playerOx, oy: playerOy, offsetX: playerOffsetX, offsetY: playerOffsetY };



}


function drawPlayersCards() {
    var player1CardInfos = getPlayerOriginalAndPositions(player1Area);
    for (var i = 0; i < player1Cards; i++) {
        var card = player1CardInfos.card.clone();
        card.x = player1CardInfos.ox;
        card.y = player1CardInfos.oy + player1CardInfos.offsetY * (i + (10 - player1Cards) / 2);
        playersCard.push(card);
        stage.addChild(card);
    }

    var player2CardInfos = getPlayerOriginalAndPositions(player2Area);
    for (var i = 0; i < player2Cards; i++) {
        var card = player2CardInfos.card.clone();
        card.x = player2CardInfos.ox + player2CardInfos.offsetX * (i + (10 - player2Cards) / 2);
        card.y = player2CardInfos.oy;
        playersCard.push(card);
        stage.addChild(card);
    }

    var player3CardInfos = getPlayerOriginalAndPositions(player3Area);
    for (var i = 0; i < player3Cards; i++) {
        var card = player3CardInfos.card.clone();
        card.x = player3CardInfos.ox;
        card.y = player3CardInfos.oy + player3CardInfos.offsetY * (i + (10 - player3Cards) / 2);
        playersCard.push(card);
        stage.addChild(card);
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
        console.log("HOME BUDDY");
        return 0;
    }
    else if (within(xPers, yPers, boardArea) && canPlay) {

        console.log("BOARD");
        return 1;
    }
    else {
        console.log("NOWHERE");
        return 2;
    }
    //console.log(x + " | " + y);
}


function init() {
    stage = new createjs.Stage("c");

    // this lets our drag continue to track the mouse even when it leaves the canvas:
    // play with commenting this out to see the difference.
    stage.mouseMoveOutside = true;
    stage.enableMouseOver(10);
    //var circle = new createjs.Shape();
    //circle.graphics.beginFill("red").drawCircle(0, 0, 50);
    var bitmap = new createjs.Bitmap("/Images/Cards/Clubs/As.png");
    bitmap.selectable = true;

    var gameBoard = new createjs.Bitmap("/Images/GameBoard.png");

    var widthFactor = getScalingFactor(stage.canvas.width, boardArea.width, gameBoard.image.width);
    var heightFactor = getScalingFactor(stage.canvas.height, boardArea.height, gameBoard.image.height);
    gameBoard.x = stage.canvas.width * boardArea.x;
    gameBoard.y = stage.canvas.height * boardArea.y;
    gameBoard.scaleX = widthFactor;
    gameBoard.scaleY = heightFactor;
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
            updateBoard();
            stage.update();
        }
        //var o = evt.target;
        //console.log(o.offset.x);

        //console.log(stage.canvas.width);
        /*if(area == 0)
        {
        
            bitmap.scaleX = playerHandScalling;
            bitmap.scaleY = playerHandScalling;
        }*/



    });

    stage.addChild(gameBoard);
    stage.addChild(bitmap);
    updateBoard();
    stage.update();
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




