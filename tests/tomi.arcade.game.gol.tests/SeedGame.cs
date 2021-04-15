using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace tomi.arcade.game.gol.tests
{
    public class SeedGameTests
    {
        //[Fact]
        //public void Seed50()
        //{
        //    GameOfLife gameOfLife = new GameOfLife(Guid.NewGuid(), 200, 200);
        //    IEnumerable<int> nextState = gameOfLife.SeedGame(50);

        //    double liveCount = gameOfLife.CurrentGameState.Count(state => state == true);
        //    double deadCount = gameOfLife.CurrentGameState.Count(state => state == false);

        //    Assert.True(nextState.Count() == liveCount);
        //    Assert.True((liveCount / gameOfLife.CurrentGameState.Length) > .49d);
        //    Assert.True((deadCount / gameOfLife.CurrentGameState.Length) > .49d);
        //}


        [Fact]
        public void GetNextState()
        {
            GameOfLife gameOfLife = new GameOfLife(Guid.NewGuid(), 200, 200);
            var nextState = gameOfLife.SeedGame(50);
            Debug.WriteLine($"NextState: {nextState.Count()}");

            for (int i = 0; i < 100; i++)
            {
                nextState = gameOfLife.SpawnNextGeneration();
                Debug.WriteLine($"NextState: {nextState.Count()}");
            }
        }
    }
}
