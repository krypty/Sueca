
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



function updateData() {
    var playerToken = $("#tokenInput").val();
    if (playerToken != "") {
        console.log("PLAYER TOEK : " + playerToken);

        var roomId = getUrlParameter("roomId");
        if (roomId != "") {
            $.ajax({
                url: getRoomState,
                data: { roomId: roomId, playerToken: playerToken },
                type: 'POST',
                success: function(data) {

                    //Object { roomState: 0, getPlayerState: Array[4], roomId: "bee5a4d3", playerToken: "ec7362e1-09db-449b-bff1-6da20f6e1d32", playerNumber: 0 }
                    //console.log("token : " + data.playerToken);
                    _playerNumer = data.playerNumber;
                    //console.log("number : " + data.playerNumber);
                    //console.log("room : " + data.playerToken);
                    setPlayersState(_titlePlayer1, data.getPlayerState[1], 1);
                    setPlayersState(_titlePlayer2, data.getPlayerState[2], 2);
                    setPlayersState(_titlePlayer3, data.getPlayerState[3], 3);
                    console.log(data.getPlayerState);
                    _gameState = data.roomState;
                }
            });
        }
        console.log(roomId);
        setInterval(function () {
            $.ajax({
                url: getRoomState,
                data: { roomId: roomId, playerToken: playerToken },
                type: 'POST',
                success: function (data) {

                    //Object { roomState: 0, getPlayerState: Array[4], roomId: "bee5a4d3", playerToken: "ec7362e1-09db-449b-bff1-6da20f6e1d32", playerNumber: 0 }
                    //console.log("token : " + data.playerToken);
                    _playerNumer = data.playerNumber;
                    //console.log("number : " + data.playerNumber);
                    //console.log("room : " + data.playerToken);
                    setPlayersState(_titlePlayer1, 1, data.getPlayerState);
                    setPlayersState(_titlePlayer2, 2, data.getPlayerState);
                    setPlayersState(_titlePlayer3, 3, data.getPlayerState);
                    _gameState = data.roomState;
                }
            });
        }, 10000);
    }
}









$(document).ready(function () {
    /*$.ajax({
        url: "database/update.html",
        context: document.body,
        success: function () {
            alert("done");
        }
    });*/
    
    
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



