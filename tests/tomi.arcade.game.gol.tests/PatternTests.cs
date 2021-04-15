using System;
using System.Linq;
using Xunit;

namespace tomi.arcade.game.gol.tests
{
    public class PatternTests
    {
        // https://en.wikipedia.org/wiki/Conway's_Game_of_Life
        [Fact]
        public void SillLife1()
        {
            var stillLife1 = new int[] { 6, 7, 10, 11 };

            GameOfLife gameOfLife = new GameOfLife(Guid.NewGuid(), 4, 4);
            gameOfLife.SetCurrentGameState(stillLife1);

            for (int cellIndex = 0; cellIndex < gameOfLife.CurrentGameState.Length; cellIndex++)
            {
                bool cellValue = gameOfLife.CurrentGameState[cellIndex];
                if (stillLife1.Contains(cellIndex))
                {
                    Assert.True(cellValue);
                }
                else
                {
                    Assert.False(cellValue);
                }
            }

            for (int iteration = 0; iteration < 10; iteration++)
            {
                gameOfLife.SpawnNextGeneration();
                for (int cell = 0; cell < gameOfLife.CurrentGameState.Length; cell++)
                {
                    bool cellValue = gameOfLife.CurrentGameState[cell];
                    if (stillLife1.Contains(cell))
                    {
                        Assert.True(cellValue);
                    }
                    else
                    {
                        Assert.False(cellValue);
                    }
                }
            }
        }
    }
}
