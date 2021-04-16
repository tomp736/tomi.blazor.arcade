GameOfLife = (function () {

    //Private members
    var _canvas;
    var _context;

    var _width;
    var _height;

    var initGame = function (canvasId, width, height) {
        _width = width;
        _height = height;

        _canvas = document.querySelector(canvasId);
        _context = _canvas.getContext('2d');
        _canvas.width = _width;
        _canvas.height = _height;
    }

    return {

        initCanvas: function (canvasId, width, height) {
            initGame(canvasId, width, height);
        },

        setGameState: function (gameState, liveColor, deadColor) {
            var liveCells = [];
            var deadCells = [];

            // console.log(gameState);

            for (i = 0; i <= gameState.length - 1; i++) {
                var cellIndex = Math.abs(gameState[i])
                var cellState = gameState[i] > 0
                var x = cellIndex % _width;
                var y = (cellIndex - x) / _width;

                if (cellState) {
                    liveCells.push({ x, y });
                }
                else {
                    deadCells.push({ x, y });
                }
            }

            this.draw(liveCells, liveColor);
            this.clear(deadCells);
        },

        draw: function (cells, color) {
            _context.fillStyle = color;
            for (li = 0; li <= cells.length - 1; li++) {
                _context.fillRect(cells[li].x, cells[li].y, 1, 1)
            }
        },

        clear: function (cells) {
            for (li = 0; li <= cells.length - 1; li++) {
                _context.clearRect(cells[li].x, cells[li].y, 1, 1)
            }
        }
    }
})();