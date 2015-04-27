function updateData(){
    setInterval(function () {
        $.ajax({
            url: getGameState,
            data: { roomId: roomId, playerToken: playerToken},
            type: 'POST',
            success: function (data) {
                console.log(data);
            }
        });
    }, 2000);
}




$(document).ready(function () {
    /*$.ajax({
        url: "database/update.html",
        context: document.body,
        success: function () {
            alert("done");
        }
    });*/
    init();
    updateData();
    //alert("LOADED");
});



