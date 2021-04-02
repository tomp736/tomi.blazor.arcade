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
        public bool[] CurrentGeneration { get; private set; }

        private bool[] nextGeneration;
        #endregion

        #region Constructors

        public GameOfLife(Guid gameId, int width, int height)
        {
            GameId = gameId;
            Width = width;
            Height = height;
            SeedGame();
        }
        #endregion

        #region Private

        private void SeedGame()
        {
            // Initiate the current and next generation boards
            CurrentGeneration = new bool[Width * Height];
            nextGeneration = new bool[Width * Height];

            // Cycle cells using rng to set live/dead cells
            var rng = new Random();

            for (int i = 0; i < CurrentGeneration.Length; i++)
            {
                // Random Board
                if (rng.Next(1, 101) < 95)
                    CurrentGeneration[i] = false;
                else
                    CurrentGeneration[i] = true;
            }
        }

        public void SetState(int[] gameState)
        {
            for (int i = 0; i < gameState.Length; i++)
            {
                int index = Math.Abs(gameState[i]);
                bool state = gameState[i] > 0;
                CurrentGeneration[index] = state;
            }
        }

        private void TransferNextGenerations()
        {
            Array.Copy(CurrentGeneration, nextGeneration, nextGeneration.Length);
        }

        private int CalculateLiveNeighbours(int cell)
        {
            // Calculate live neighours
            int[] neighbors = new int[]
            {
                cell - cell - Width - 1,    // top left
                cell - cell - Width,        // top mid
                cell - cell - Width + 1,    // top right                
                cell - 1,                   // left
                cell + 1,                   // right
                cell - cell + Width - 1,    // bot left
                cell - cell + Width,        // bot mid
                cell - cell + Width + 1,    // bot right
            };

            int liveNeighbours = 0;
            foreach (var neighbor in neighbors.Where(n => n >= 0))
            {
                liveNeighbours += CurrentGeneration[neighbor] ? 1 : 0;
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
            for (int i = 0; i < CurrentGeneration.Length; i++)
            {
                int liveNeighbours = CalculateLiveNeighbours(i);
                if (CurrentGeneration[i] == true && liveNeighbours < 2)
                    nextGeneration[i] = false;

                else if (CurrentGeneration[i] == true && liveNeighbours > 3)
                    nextGeneration[i] = false;

                else if (CurrentGeneration[i] == false && liveNeighbours == 3)
                    nextGeneration[i] = true;

                else
                    nextGeneration[i] = CurrentGeneration[i];
            }

            TransferNextGenerations();
        }
        #endregion
    }
}
