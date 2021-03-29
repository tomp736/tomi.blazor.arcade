GameOfLife = (function () {

    //Private members
    var _canvas;
    var _context;

    var _width;
    var _height;

    var _lastState;

    var m_canvas;
    var m_context;

    var m_drawing;

    var initGame = function (canvasId, width, height) {
        _width = width;
        _height = height;

        _canvas = document.querySelector(canvasId);
        _context = _canvas.getContext('2d');
        _canvas.width = _width;
        _canvas.height = _height;

        m_canvas = document.createElement('canvas');
        m_context = m_canvas.getContext('2d');
        m_canvas.width = _width;
        m_canvas.height = _height;
    }

    return {

        initCanvas: function (canvasId, width, height) {
            initGame(canvasId, width, height);
        },

        setGameState: function (gameState) {
            var liveCells = [];
            var deadCells = [];

            for (i = 0; i <= gameState.length - 1; i++) {
                var cellState = gameState[i].value;
                var x = gameState[i].key % _width;
                var y = (gameState[i].key - x) / _width;

                if (cellState) {
                    liveCells.push({ x, y });
                }
                else {
                    deadCells.push({ x, y });
                }
            }

            this.drawLive(liveCells);
            this.drawDead(deadCells);

            //_context.clearRect(0, 0, _width, _height)
            //_context.drawImage(m_canvas, 0, 0);
        },

        drawLive: function (cells) {
            _context.fillStyle = "black";
            for (li = 0; li <= cells.length - 1; li++) {
                _context.fillRect(cells[li].x, cells[li].y, 1, 1)
            }
        },

        drawDead: function (cells) {
            _context.fillStyle = "grey";
            for (li = 0; li <= cells.length - 1; li++) {
                _context.fillRect(cells[li].x, cells[li].y, 1, 1)
            }
        }
    }
})();