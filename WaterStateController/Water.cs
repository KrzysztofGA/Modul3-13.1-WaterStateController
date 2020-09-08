using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterStateController
{
    public class Water
    {
        public double Amount { get; private set; }
        public double Temperature { get; private set; }
        public double ProportionFirstState { get; private set; }
        public WaterState State { get; private set; }

        public Water(double amount, double temperature, double? proportion = null)
        {
            Amount = amount;
            Temperature = temperature;
            State = ActualState(temperature);

            // It will work for all values ​​other than 0 and 100,
            // and terminates the constructor at this point
            if (Temperature != 100 && Temperature != 0) return;

            // Only 0 and 100 temperatures will go here

            // If proportion is specified, this code will be skipped
            // and no exception message will be displayed
            if (proportion == null)
                throw new ArgumentException
                ("When temperature is 0 or 100, you must provide " +
                 "a value for proportion");

            ProportionFirstState = proportion.Value;
            State = SetProportionState(temperature);
        }

        private WaterState ActualState(double temperature) =>
            (temperature <= 0) ? WaterState.Ice
            : (temperature >= 100) ? WaterState.Gas : WaterState.Fluid;

        private WaterState SetProportionState(double temperature)
        {
            switch (ProportionFirstState)
            {
                // All water remains dependent on the starting temperature
                case 1:
                    return State;
                // If PFS is 0 then when the temperature is 0 it means that
                // all the water has already turned into liquid if the temperature
                // is equal to 100 means that the liquid has already
                // turned into a gaseous state
                case 0:
                    return temperature == 0 ? WaterState.Fluid : WaterState.Gas;
                // With proportions (0, 1) and a temperature of 0 it means that
                // we have a mix of water and ice, and at a temperature of 100
                // we have a mix of water and gas
                default:
                    return temperature == 0 ? WaterState.IceAndFluid : WaterState.FluidAndGas;
            }
        }

        public void AddEnergy(int energy)
        {
            
        }
    }
}