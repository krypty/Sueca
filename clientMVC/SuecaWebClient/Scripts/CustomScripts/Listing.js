function onAjaxBegin() {
    $("#loading").removeClass("hidden");
}

function onAjaxComplete() {
    $("#loading").addClass("hidden");
    
}

function onSuccessGetKey(data) {
    $("#keyArea").removeClass("hidden");
    $("#keyLinkArea").removeClass("hidden");
    $("#roomJoinArea").removeClass("hidden");
    $("#requestButtonGroup").addClass("hidden");
    $("#keyField").val(data);
    var roomUrlString = roomPath+"?roomId="+data;
    $("#keyLinkField").val(roomUrlString);
    $("#joinRoomLink").attr("href", roomUrlString);
}

function onFailureGetKey(){
    $("#alert-area").removeClass("hidden");
    $("#alert-area").val("Impossibile de récupérer une clé, le serveur est peut-être indisponible");
    $("#requestButton").val("Réessayer");
}