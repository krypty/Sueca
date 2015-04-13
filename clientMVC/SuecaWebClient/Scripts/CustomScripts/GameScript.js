

var CanvasWidth;
var CanvasHeight;



function loadCards(context, cardObj)
{
    base_image = new Image();
    base_image.src = cardObj.image;
    base_image.onload = function () {
        context.drawImage(base_image, 100, 100);
    }
    return;
}






function canvasHandle()
{
    


}
(function () {
    var
        htmlCanvas = document.getElementById('c'),

        context = htmlCanvas.getContext('2d');

    // Start listening to resize events and
    // draw canvas.
     initialize();

    function initialize() {
        // Register an event listener to
        // call the resizeCanvas() function each time 
        // the window is resized.
        window.addEventListener('resize', resizeCanvas, false);

        // Draw canvas border for the first time.
        resizeCanvas();

        loadCards(context, card);



    }

    function redraw()
    {

    }


    function resizeCanvas() {
        var canevas = document.getElementById('c');
        CanvasWidth = canevas.width;
        CanvasHeight = canevas.height;
        redraw();
    }

})();




