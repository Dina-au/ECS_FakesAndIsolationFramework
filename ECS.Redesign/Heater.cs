namespace ECS
{
    public class Heater : IHeater
    {
        public bool RunSelfTest()
        {
            return true;
        }

        public void TurnOff()
        {
            System.Console.WriteLine("Heater is off");
        }

        public void TurnOn()
        {
            System.Console.WriteLine("Heater is on");
        }
    }
}