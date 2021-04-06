using System;
using System.Collections.Generic;
using System.Linq;

namespace tomi.arcade.game.gol
{
    public class GameOfLife
    {
        #region Properties & Fields
        Guid GameId { get; set; }
        public int Width { get; }
        public int Height { get; }
        public bool[] CurrentGameState { get; private set; }

        #endregion

        #region Constructors

        public GameOfLife(Guid gameId, int width, int height)
        {
            GameId = gameId;
            Width = width;
            Height = height;
            CurrentGameState = new bool[Width * Height];
        }

        #endregion

        #region Private

        private int CalculateLiveNeighbours(int cell)
        {
            // Calculate live neighours
            int[] neighbors = new int[]
            {
                cell - Width - 1,    // top left
                cell - Width,        // top mid
                cell - Width + 1,    // top right                
                cell - 1,            // left
                cell + 1,            // right
                cell + Width - 1,    // bot left
                cell + Width,        // bot mid
                cell + Width + 1     // bot right
            };

            int liveNeighbours = 0;
            foreach (var neighbor in neighbors.Where(n => n >= 0 && n <= CurrentGameState.Length - 1))
            {
                liveNeighbours += CurrentGameState[neighbor] ? 1 : 0;
            }
            return liveNeighbours;
        }
        #endregion

        #region Public
        public void Clear()
        {
            CurrentGameState = new bool[Width * Height];
        }

        public IEnumerable<int> SeedGame(int fillPercentage)
        {
            // Initiate the current and next generation boards
            CurrentGameState = new bool[Width * Height];

            // Cycle cells using rng to set live/dead cells
            var rng = new Random();

            // Random Board
            IEnumerable<int> nextCellStates = Enumerable
                .Range(0, CurrentGameState.Length)
                .Where(n => rng.NextDouble() <= fillPercentage / 100d)
                .Select(n => n).ToList();

            SetCurrentGameState(nextCellStates);
            return nextCellStates;
        }

        public IEnumerable<int> SpawnNextGeneration()
        {
            List<int> nextCellStates = new List<int>();

            for (int i = 0; i < CurrentGameState.Length; i++)
            {
                int liveNeighbours = CalculateLiveNeighbours(i);

                if (CurrentGameState[i] == true && liveNeighbours < 2)
                    nextCellStates.Add(-i);
                else if (CurrentGameState[i] == true && liveNeighbours > 3)
                    nextCellStates.Add(-i);
                else if (CurrentGameState[i] == false && liveNeighbours == 3)
                    nextCellStates.Add(i);
                    // no change, do nothing
                    // _nextGeneration[i] = CurrentGameState[i];
            }

            SetCurrentGameState(nextCellStates);
            return nextCellStates;
        }

        public void SetCurrentGameState(IEnumerable<int> cellStates)
        {
            foreach(int cellState in cellStates)
            {
                CurrentGameState[Math.Abs(cellState)] = cellState > 0;
            }
        }

        #endregion
    }
}
