namespace ECS
{
    public class ECS
    {
        private readonly ITempSensor _tempSensor;
        private readonly IHeater _heater;
        public int TemperatureThreshold { get; private set; }

        public ECS(ITempSensor tempSensor, IHeater heater, int temperatureThreshold)
        {
            _tempSensor = tempSensor;
            TemperatureThreshold = temperatureThreshold;
            _heater = heater;
        }


        public void Regulate()
        {
            var curTemp = _tempSensor.GetTemp();
            if (curTemp < TemperatureThreshold) _heater.TurnOn();
            else _heater.TurnOff();
        }

        public bool RunSelfTest()
        {
            return _tempSensor.RunSelfTest() && _heater.RunSelfTest();
        }


    }
}
