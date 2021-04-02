using System;
using System.Linq;

namespace tomi.arcade.game.gol
{
    // https://github.com/Kohana55/ConwaysGameOfLife
    public class GameOfLife
    {
        #region Properties & Fields
        Guid GameId { get; set; }
        public int Width { get; }
        public int Height { get; }
        public bool[,] CurrentGeneration { get; private set; }

        private bool[,] nextGeneration;
        #endregion

        #region Constructors

        /// <summary>
        /// Ctor that accepts a custom board size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GameOfLife(Guid gameId, int width, int height)
        {
            GameId = gameId;
            Width = width;
            Height = height;
            SeedGame();
        }
        #endregion

        #region Private
        /// <summary>
        /// Seed Game with live and dead cells
        /// Whereby the live cells occupy approx. 20% of the board
        /// </summary>
        private void SeedGame()
        {
            // Initiate the current and next generation boards
            CurrentGeneration = new bool[Width, Height];
            nextGeneration = new bool[Width, Height];

            // Cycle cells using rng to set live/dead cells
            var rng = new Random();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    // Random Board
                    if (rng.Next(1, 101) < 95)
                        CurrentGeneration[i, j] = false;
                    else
                        CurrentGeneration[i, j] = true;
                }
            }
        }

        public void SetState(int[] gameState)
        {
            for (int i = 0; i < gameState.Length; i++)
            {
                int index = Math.Abs(gameState[i]);
                bool state = gameState[i] > 0;
                int x = index % Width;
                int y = index - x / Width;
                CurrentGeneration[x, y] = state;
            }
        }

        /// <summary>
        /// Transfer next generation to current generation 
        /// </summary>
        private void TransferNextGenerations()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    CurrentGeneration[i, j] = nextGeneration[i, j];
                }
            }
        }

        /// <summary>
        /// Given any cell - calculate live neighbours
        /// </summary>
        /// <param name="x">X coord of Cell</param>
        /// <param name="y">Y coord of Cell</param>
        /// <returns></returns>
        private int CalculateLiveNeighbours(int x, int y)
        {
            // Calculate live neighours
            int liveNeighbours = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || x + i >= Width)   // Out of bounds
                        continue;
                    if (y + j < 0 || y + j >= Height)   // Out of bounds
                        continue;
                    if (x + i == x && y + j == y)       // Same Cell
                        continue;

                    // Add cells value to current live neighbour count
                    liveNeighbours += CurrentGeneration[x + i, y + j] ? 1 : 0;
                }
            }

            return liveNeighbours;
        }
        #endregion

        #region Public
        /// <summary>
        /// Spawn the next generation
        /// </summary>
        public void SpawnNextGeneration()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int liveNeighbours = CalculateLiveNeighbours(x, y);

                    if (CurrentGeneration[x, y] == true && liveNeighbours < 2)
                        nextGeneration[x, y] = false;

                    else if (CurrentGeneration[x, y] == true && liveNeighbours > 3)
                        nextGeneration[x, y] = false;

                    else if (CurrentGeneration[x, y] == false && liveNeighbours == 3)
                        nextGeneration[x, y] = true;

                    else
                        nextGeneration[x, y] = CurrentGeneration[x, y];

                }
            }

            TransferNextGenerations();
        }
        #endregion
    }
}
