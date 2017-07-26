using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CheCoxshall.Battleship.Core.Test
{
    [Binding]
    public class BattleshipsSteps
    {
        private GameBoard _gameBoard = new GameBoard();

        [Given(@"I have placed(?:\san\s|\sa\s)([a-zA-Z]+) (horizontally|vertically) at cell ([A-J]\d)")]
        public void GivenIHavePlacedAShip(ShipType shipType, string orientation, Cell cell)
        {
            try
            {
                _gameBoard.PlaceShip(shipType, cell, orientation == "horizontally");
            }
            catch (CannotPlaceException ex)
            {
                ScenarioContext.Current.Set(ex);
            }
        }

        [Given(@"I have placed the following ships")]
        public void GivenIHavePlacedTheFollowingShips(Table table)
        {
            var set = table.CreateSet<ShipPlacement>();
            foreach (var item in set)
            {
                try
                {
                    _gameBoard.PlaceShip(item.ShipType, item.Cell, item.IsHorizontal);
                }
                catch(CannotPlaceException ex)
                {
                    ScenarioContext.Current.Set(ex);
                }
            }
        }


        [When(@"A shot lands at cell ([A-J]\d)")]
        public void WhenAShotLandsAtCell(Cell cell)
        {
            ScenarioContext.Current.Set(_gameBoard.RegisterShot(cell));
        }
       

        [Then(@"the game board should(.*) be ready")]
        public void CheckGameBoardReadiness(string not)
        {
            if (!String.IsNullOrWhiteSpace(not))
                Assert.IsFalse(_gameBoard.Ready);
            else
                Assert.IsTrue(_gameBoard.Ready);
        }

        [Then(@"My ([a-zA-Z]+) should be sunk")]
        public void ThenMyShipShouldBeSunk(ShipType shipType)
        {
            var lastShotResult = ScenarioContext.Current.Get<ShotResult>();
            Assert.AreEqual(shipType, lastShotResult.Ship);
            Assert.IsTrue(lastShotResult.IsSunk);
        }

        [Then(@"the game board should have thrown a CannotPlaceException with the message '(.*)'")]
        public void ThenTheGameBoardShouldHaveThrownACannotPlaceExceptionWithTheMessage(string exceptionMessage)
        {
            if (ScenarioContext.Current.TryGetValue(out CannotPlaceException exception))
                Assert.AreEqual(exceptionMessage, exception.Message);
            else
                Assert.Fail("Expected exception was not found");
        }



        [StepArgumentTransformation]
        public Cell CellTransform(string cellRef)
        {
            return new Cell(cellRef[0], int.Parse(cellRef[1].ToString()));
        }
    }

    public class ShipPlacement
    {
        public ShipType ShipType { get; set; }
        public bool IsHorizontal { get { return Orientation == "horizontally"; } }
        public string Orientation { get; set; }
        public Cell Cell { get; set; }
    }
}
