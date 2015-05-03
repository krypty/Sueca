
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
        
    
        setInterval(function () {
            $.ajax({
                url: getRoomState,
                data: { roomId: roomId, playerToken: playerToken },
                type: 'POST',
                success: function (data) {
                    console.log(data);
                }
            });
        }, 2000);
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



