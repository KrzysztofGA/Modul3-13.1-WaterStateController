using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaterStateController;

namespace WaterStateControllerTests
{
    [TestClass]
    public class WaterStateControllerTest
    {
        [TestMethod]
        public void Test01WaterAt20Degrees()
        {
            var water = new Water(50, 20);
            Assert.AreEqual(WaterState.Fluid, water.State);
            Assert.AreEqual(20, water.Temperature);
            Assert.AreEqual(50, water.Amount);
        }

        [TestMethod]
        public void Test02WaterAtMinus20Degrees()
        {
            var water = new Water(50, -20);
            Assert.AreEqual(WaterState.Ice, water.State);
            Assert.AreEqual(-20, water.Temperature);
        }

        [TestMethod]
        // Tester om tilstanden blir gass ved 120 grader
        public void Test03WaterAt120Degrees()
        {
            var water = new Water(50, 120);
            Assert.AreEqual(WaterState.Gas, water.State);
            Assert.AreEqual(120, water.Temperature);
        }

        [TestMethod]
        // Ved 0 og 100 grader må vi angi en frivillig parameter til konstruktøren som angir hvor stor del 
        // som er i den første fasen (altså is ved blanding av is og flytende - og flytende ved blanding 
        // av flytende og gass). Denne testen sjekker at vi får exception om dette ikke er angitt og temperaturen
        // er 100 grader. 
        [ExpectedException(typeof(ArgumentException),
            "When temperature is 0 or 100, you must provide a value for proportion")]
        public void Test04WaterAt100DegreesWithoutProportion()
        {
            var water = new Water(50, 100);
        }

        [TestMethod]
        // Sjekker at vi får miks av flytende og gass, med 30% av det første
        public void Test05WaterAt100Degrees()
        {
            var water = new Water(50, 100, 0.3);
            Assert.AreEqual(WaterState.FluidAndGas, water.State);
            Assert.AreEqual(100, water.Temperature);
            Assert.AreEqual(0.3, water.ProportionFirstState);
        }

        [TestMethod]
        public void Test06WaterAt100Degrees()
        {
            var water = new Water(50, 100, 1);
            Assert.AreEqual(WaterState.Gas, water.State);
            Assert.AreEqual(100, water.Temperature);
            Assert.AreEqual(1, water.ProportionFirstState);
        }

        [TestMethod]
        public void Test07WaterAt100Degrees()
        {
            var water = new Water(50, 100, 0);
            Assert.AreEqual(WaterState.Gas, water.State);
            Assert.AreEqual(100, water.Temperature);
            Assert.AreEqual(0, water.ProportionFirstState);
        }

        [TestMethod]
        // Tester at når vi tilfører energi, så stiger temperaturen med riktig antall grader
        public void Test08AddEnergy1()
        {
            var water = new Water(4, 10);
            water.AddEnergy(10);
            Assert.AreEqual(12.5, water.Temperature);
        }
    }
}