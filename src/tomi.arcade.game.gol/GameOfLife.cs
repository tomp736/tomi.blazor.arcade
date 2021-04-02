using System;
using System.Linq;

namespace tomi.arcade.game.gol
{
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

            // Random Board
            CurrentGeneration = Enumerable.Range(0, CurrentGeneration.Length)
                .Select(n => rng.Next(1, 101) < 95).ToArray();
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

        public void SpawnNextGeneration()
        {
            for (int i = 0; i < CurrentGeneration.Length; i++)
            {
                int liveNeighbours = CalculateLiveNeighbours(i);

                // dies is alive and alone, or alive and crowded
                if (CurrentGeneration[i] == true && (liveNeighbours < 2 || liveNeighbours > 3))
                    nextGeneration[i] = false;
                // spawns if dead and has exactly three neighbors
                else if (CurrentGeneration[i] == false && liveNeighbours == 3)
                    nextGeneration[i] = true;
                // otherwise nothing happens
                else
                    nextGeneration[i] = CurrentGeneration[i];
            }

            TransferNextGenerations();
        }

        public void SetState(int[] gameState)
        {
            for (int i = 0; i < gameState.Length; i++)
            {
                CurrentGeneration[Math.Abs(gameState[i])] = gameState[i] > 0;
            }
        }

        #endregion
    }
}
