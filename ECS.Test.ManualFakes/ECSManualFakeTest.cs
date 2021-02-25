using NUnit.Framework;

namespace ECS.Test.ManualFakes
{
    [TestFixture]
    public class EcsManualFakeTest
    {
        private FakeHeater _fakeHeater;
        private FakeTempSensor _fakeTempSensor;
        private ECS _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeHeater = new FakeHeater();
            _fakeTempSensor = new FakeTempSensor();
            _uut = new ECS(_fakeTempSensor, _fakeHeater, 23);
        }

        #region TempLow
        [Test]
        public void Regulate_TempIsLow_HeaterIsTurnedOn()
        {
            // Setup stub with desired response
            _fakeTempSensor.Temp = 20;
            // Act
            _uut.Regulate();

            // Assert on the mock - was the heater called correctly
            Assert.That(_fakeHeater.TurnOnCalledTimes, Is.EqualTo(1));
        }

        #endregion

        #region TempHigh
        [Test]
        public void Regulate_TempIsAboveUpperThreshold_HeaterIsTurnedOff()
        {
            // Setup the stub with desired response
            _fakeTempSensor.Temp = 27;
            _uut.Regulate();

            // Assert on the mock - was the heater called correctly
            Assert.That(_fakeHeater.TurnOffCalledTimes, Is.EqualTo(1));
        }

        #endregion

        #region SelfTest
        [TestCase(true, true, true)]
        [TestCase(true, false, false)]
        [TestCase(false, true, false)]
        [TestCase(false, false, false)]
        public void RunSelfTest_CombinationOfInput_CorrectOutput(
            bool tempResult, bool heaterResult,bool expectedResult)
        {
            _fakeTempSensor.SelfTestResult = tempResult;
            _fakeHeater.SelfTestResult = heaterResult;

            Assert.That(_uut.RunSelfTest(), Is.EqualTo(expectedResult));
        }





        #endregion


    }
}