var SuecaCard;
(function (SuecaCard) {
    var RoomManager = (function () {
        function RoomManager() {
        }
        RoomManager.prototype.initImages = function (imageList) {
            for (var img in imageList) {
                var imageStoring = new Image(); // Crée un nouvel objet Image
                imageStoring.src = 'myImage.png'; // Définit le chemin vers sa source
                //imageStoring.onlo
                imageStoring.onload = function () {
                    // instructions appelant drawImage ici
                };
            }
        };
        return RoomManager;
    })();
    SuecaCard.RoomManager = RoomManager;
})(SuecaCard || (SuecaCard = {}));
//# sourceMappingURL=RoomManager.js.map