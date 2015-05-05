
function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}




function playCard(c)
{
    console.log("Playing Card");
    console.log(c);
    $.ajax({
        url: playCardPath,
        data: { playerToken: _playerToken, color: c.CardColor, value: c.CardValue },
        type: 'POST',
        success: function (data) {
            _getRoomState();
        }
    });


}

function sendReady() {

    _imReady = !_imReady;
    console.log("Ready : " + _imReady);
    $.ajax({
        url: sendReadyPath,
        data: { playerToken: _playerToken, state : _imReady},
        type: 'POST',
        success: function (data) {
            _getRoomState();
        }
        
    });

}

var _playerToken = "";
var _roomId = "";


function setCouleur(color) {
    var imageBlock = $("#imageCouleur");
    if (color == "")
    {
        if (!imageBlock.hasClass("hidden")) {
            imageBlock.addClass("hidden");
        }

    }
    else
    {
        if (imageBlock.hasClass("hidden")) {
            imageBlock.removeClass("hidden");
        }
    }
    

    switch(color)
    {
        case "Hearts":
            imageBlock.attr("src", "/Images/Colors/Hearts.png");
            break;
        case "Clubs":
            imageBlock.attr("src", "/Images/Colors/Clubs.png");
            break;
        case "Diamonds":
            imageBlock.attr("src", "/Images/Colors/Diamonds.png");
            break;
        case "Spades":
            imageBlock.attr("src", "/Images/Colors/Spades.png");
            break;
        default:
            imageBlock.attr("src", "");
           
            
            break;
    }


}



function _getRoomState()
{
    $.ajax({
        url: getRoomState,
        data: { roomId: _roomId, playerToken: _playerToken },
        type: 'POST',
        success: function (data) {
            //console.log(data);
            _playerNumer = data.playerNumber;

            //_imReady =  > 1 ? true : false;
            if (data.getPlayerState[0] == 2){
                $("#btnReady").val("Je ne suis plus prêt");
                _imReady = true;
            }
                
            else if (data.getPlayerState[0] == 1) {
                $("#btnReady").val("Je suis prêt");
                _imReady = false;
            }


            setPlayersState(_titlePlayer1, data.getPlayerState[1], 1);
            setPlayersState(_titlePlayer2, data.getPlayerState[2], 2);
            setPlayersState(_titlePlayer3, data.getPlayerState[3], 3);
            _gameState = data.roomState;

            if (_gameState == 1) {
                $.ajax({
                    url: getGameState,
                    data: { roomId: _roomId, playerToken: _playerToken },
                    type: 'POST',
                    success: function (dataRoom) {

                        //Object { roomState: 0, getPlayerState: Array[4], roomId: "bee5a4d3", playerToken: "ec7362e1-09db-449b-bff1-6da20f6e1d32", playerNumber: 0 }
                        //console.log("token : " + data.playerToken);
                        console.log(dataRoom);
                        //console.log("NO UPDATE WAT ? ");
                        player1Cards = dataRoom.nbCards[1];
                        player2Cards = dataRoom.nbCards[2];
                        player3Cards = dataRoom.nbCards[2];


                        _playerTurn = dataRoom.playerTurn;

                        //TODO ADD CARDS ON BOARD
                        
                        //newCardsOnBoard(data.listPlayedCards);
                        //console.log("forboucle?");
                        if (dataRoom.listPlayedCards != null)
                        {
                            for (var i = 0; i < 4; i++) {
                                if (dataRoom.listPlayedCards[i] != null)
                                    drawCardsOnBoard(i, cardsColor[dataRoom.listPlayedCards[i].color][dataRoom.listPlayedCards[i].value]);
                                else
                                    drawCardsOnBoard(i, null);
                            }
                        }
                        
                        
                        //_roundColor = dataRoom.colorGame;
                        setCouleur(dataRoom.colorGame);


                        //Ajout des cartes en main
                        for (var i = 0; i < dataRoom.listPlayerCards.length; i++) {
                            if (dataRoom.listPlayerCards[i] != null) {
                                var contains = false;
                                for (var j = 0; j < _playerCards.length; j++) {
                                    if (_playerCards[i] != null) {
                                        if (_playerCards[i].CardColor == dataRoom.listPlayerCards[i].color && _playerCards[i].CardValue == dataRoom.listPlayerCards[i].value) {
                                            contains = true;
                                        }
                                    }
                                }
                                if (!contains) {
                                    addHandHeldCard(cardsHand[dataRoom.listPlayerCards[i].color][dataRoom.listPlayerCards[i].value], i);
                                }
                            }
                        }


                    }
                });


            }else if(_gameState == 2)
            {
                _roundColor = "";
                setCouleur("");
                alert("SCORE !");
            }
            else {
                for (var i = 0; i < 4; i++) {
                    drawCardsOnBoard(i, null);
                }
                _roundColor = "";
                player1Cards = 0;
                player2Cards = 0;
                player3Cards = 0;
                clearPlayerCards();
                setCouleur("");
            }

        }
    });
}

function updateData() {
    var playerToken = $("#tokenInput").val();
    _playerToken = playerToken;
    var roomId = getUrlParameter("roomId");
    _roomId = roomId;
    if (_roomId != "") {
        
        
        if (_playerToken != "") {
            var btnReady = $("#btnReady");
            btnReady.removeClass('hidden');
            btnReady.click(function () {
                sendReady();
            });
            _getRoomState();
        
        console.log(_roomId);
        setInterval(function () {
            _getRoomState();
        }, 5000);
        }
        else
        {
            $("#c").addClass("hidden");
            $("#gameFullRow").removeClass("hidden");
            
        }
    }
    else
    {
        $("#c").addClass("hidden");
        $("#noRoomRow").removeClass("hidden");
    }
}









$(document).ready(function () {
    
    updateData();



    init();
    

    (function () {
        var canvas = document.getElementById('c'),
            context = canvas.getContext('2d');
        context.mozImageSmoothingEnabled = true;
        context.ImageSmoothingEnabled = true;

        function drawStuff() {
            // do your drawing stuff here
            //updateBoard();
            drawScene();
        }

        function resizeCanvas() {

            //canvas.width = window.innerWidth * 0.8;
            //canvas.height = window.innerHeight*0.8;

            /**
             * Your drawings need to be inside this function otherwise they will be reset when 
             * you resize the browser window and the canvas goes will be cleared.
             */
            drawStuff();
        }
        resizeCanvas();
        // resize the canvas to fill browser window dynamically
        window.addEventListener('resize', resizeCanvas, false);

        
    })();




    //alert("LOADED");
});



