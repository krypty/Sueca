
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




function sendReady() {

    _imReady = !_imReady;
    $.ajax({
        url: sendReadyPath,
        data: { playerToken: _playerToken, state : _imReady},
        type: 'POST',
        success: function (data) {
                
            if (!data) {
                _imReady = !_imReady;
            }
            if(_imReady)
                $("#btnReady").val("Je ne suis plus prêt");
            else
                $("#btnReady").val("Je suis prêt");

        }
        
    });

}

var _playerToken = "";
var _roomId = "";
function updateData() {
    var playerToken = $("#tokenInput").val();
    _playerToken = playerToken;
    if (playerToken != "") {
        var roomId = getUrlParameter("roomId");
        _roomId = roomId;
        if (roomId != "") {
            var btnReady = $("#btnReady");
            btnReady.removeClass('hidden');
            btnReady.click(function () {
                sendReady();
            });
            

            $.ajax({
                url: getRoomState,
                data: { roomId: _roomId, _playerToken: _playerToken },
                type: 'POST',
                success: function(data) {
                    _playerNumer = data.playerNumber;

                    setPlayersState(_titlePlayer1, data.getPlayerState[1], 1);
                    setPlayersState(_titlePlayer2, data.getPlayerState[2], 2);
                    setPlayersState(_titlePlayer3, data.getPlayerState[3], 3);
                    console.log("Players state : ");
                    console.log(data.getPlayerState);
                    _gameState = data.roomState;
                    

                }
            });
        }
        console.log(roomId);
        setInterval(function () {
            $.ajax({
                url: getRoomState,
                data: { roomId: _roomId, playerToken: _playerToken },
                type: 'POST',
                success: function (data) {
                    console.log(data);
                    //Object { roomState: 0, getPlayerState: Array[4], roomId: "bee5a4d3", playerToken: "ec7362e1-09db-449b-bff1-6da20f6e1d32", playerNumber: 0 }
                    //console.log("token : " + data.playerToken);
                    //_playerNumer = data.playerNumber;
                    //console.log("number : " + data.playerNumber);
                    //console.log("room : " + data.playerToken);
                    setPlayersState(_titlePlayer1, data.getPlayerState[1], 1);
                    setPlayersState(_titlePlayer2, data.getPlayerState[2], 2);
                    setPlayersState(_titlePlayer3, data.getPlayerState[3], 3);
                    _gameState = data.roomState;

                    if (_gameState == 1) {
                        $.ajax({
                            url: getGameState,
                            data: { roomId: roomId, playerToken: playerToken },
                            type: 'POST',
                            success: function (data) {

                                //Object { roomState: 0, getPlayerState: Array[4], roomId: "bee5a4d3", playerToken: "ec7362e1-09db-449b-bff1-6da20f6e1d32", playerNumber: 0 }
                                //console.log("token : " + data.playerToken);
                                console.log(data);


                            }
                        });


                    }

                }
            });
        }, 10000);
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



