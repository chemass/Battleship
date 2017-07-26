using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Collections.Generic;

namespace CheCoxshall.Battleship.Core.Test
{
    [Binding]
    public class BattleshipsGameplaySteps
    {
        [BeforeTestRun]
        public static void BeforeScenario()
        {
            Service.Instance.RegisterValueRetriever(new CellValueRetriever());
            Service.Instance.RegisterValueComparer(new CellValueComparer());
        }

        [Given(@"A new game is created")]
        public void GivenANewGameIsCreated()
        {
            var game = new Game();
            ScenarioContext.Current.Set(game);
            game.Hit += Game_Hit;
            game.Miss += Game_Miss;
            game.ShipDestroyed += Game_ShipDestroyed;
        }

        private void Game_Hit(object sender, EventArgs e)
        {
            ScenarioContext.Current.Add("LastShotResult", "Hit");
        }

        private void Game_Miss(object sender, EventArgs e)
        {
            ScenarioContext.Current.Add("LastShotResult", "Miss");
        }

        private void Game_ShipDestroyed(object sender, ShipDestroyedEventArgs e)
        {
            ScenarioContext.Current.Add("LastShotResult", e.DestroyedShip);
        }

        [Given(@"Player (.*) has placed the following ships")]
        public void GivenPlayerHasPlacedTheFollowingShips(int playerId, Table table)
        {
            var set = table.CreateSet<ShipPlacement>();
            var game = ScenarioContext.Current.Get<Game>();

            foreach (var item in set)
            {
                try
                {
                    game.PlaceShip(playerId - 1, item.ShipType, item.Cell, item.IsHorizontal);
                }
                catch (CannotPlaceException ex)
                {
                    ScenarioContext.Current.Set(ex);
                }
            }
        }

        [When(@"Player (\d) has taken a shot at ([A-J]{1}\d{1,2})")]
        public void WhenPlayerHasTakenAShot(int playerId, Cell targetCell)
        {
            ScenarioContext.Current.Get<Game>().TakeShot(playerId, targetCell);
        }
        
        [Then(@"It should have two game boards")]
        public void ThenItShouldHaveTwoGameBoards()
        {
            var game = ScenarioContext.Current.Get<Game>();
            Assert.IsNotNull(game.GetGameBoardRows(0));
            Assert.IsNotNull(game.GetGameBoardRows(1));
        }

        [Then(@"the game should(.*) be ready")]
        public void ThenTheGameShouldBeReady(string not)
        {
            var game = ScenarioContext.Current.Get<Game>();
            Assert.AreEqual(string.IsNullOrWhiteSpace(not), game.Ready);
        }

        [Then(@"A (hit|miss) should be registered")]
        public void ThenAHitOrMissShouldBeRegistered(string hitOrMiss)
        {
            var shotResult = ScenarioContext.Current.Get<string>("LastShotResult")?.ToLowerInvariant();
            Assert.AreEqual(hitOrMiss, shotResult);
        }

    }

    public class CellValueRetriever : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return targetType == typeof(ShipPlacement) && propertyType == typeof(Cell);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return new Cell(keyValuePair.Value[0], int.Parse(keyValuePair.Value[1].ToString()));
        }
    }

    public class CellValueComparer : IValueComparer
    {
        public bool CanCompare(object actualValue)
        {
            return actualValue is Cell;
        }

        public bool Compare(string expectedValue, object actualValue)
        {
            return false;
        }
    }

}
