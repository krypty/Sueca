
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

    //alert("LOADED");
});



