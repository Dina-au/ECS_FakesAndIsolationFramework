using NUnit.Framework;
using NSubstitute; 

namespace ECS.Test.ManualFakes
{
    [TestFixture]
    public class EcsManualFakeTest
    {
        private IHeater _fakeHeater;
        private ITempSensor _fakeTempSensor;
        private ECS _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeHeater = Substitute.For<IHeater>();
            _fakeTempSensor = Substitute.For<ITempSensor>();
            _uut = new ECS(_fakeTempSensor, _fakeHeater, 23);
        }

        #region TempLow
        [Test]
        public void Regulate_TempIsLow_HeaterIsTurnedOn()
        {
            // Setup stub with desired response
            _fakeTempSensor.GetTemp().Returns(20);

            // Act
            _uut.Regulate();

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(1).TurnOn();
        }

        #endregion

        #region TempHigh
        [Test]
        public void Regulate_TempIsAboveUpperThreshold_HeaterIsTurnedOff()
        {
            // Setup the stub with desired response
            _fakeTempSensor.GetTemp().Returns(27);

            _uut.Regulate();

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(1).TurnOff();
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
            _fakeTempSensor.RunSelfTest().Returns(tempResult);
            _fakeHeater.RunSelfTest().Returns(heaterResult);

            bool actualResult = _uut.RunSelfTest(); 
            Assert.That(actualResult,Is.EqualTo(expectedResult));

        }

        #endregion


    }
}