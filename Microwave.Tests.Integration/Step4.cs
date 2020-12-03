using System;
using System.IO;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Tests.Integration
{
    class Step4
    {
        private ICookController _cookController;
        private IPowerTube _power;
        private IDisplay _display;
        private IOutput _output;
        private IUserInterface _userInterface;
        private ITimer _timer;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _power = new PowerTube(_output);
            _display = new Display(_output);
            _userInterface = Substitute.For<IUserInterface>();
            _timer = Substitute.For<ITimer>();
            _cookController = new CookController(_timer, _display ,_power, _userInterface);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TestCase(50)]
        [TestCase(700)]
        public void StartCooking_TurnOn(int power)
        {
            int time = 60;
            _cookController.StartCooking(power, time);
            Assert.That(_stringWriter.ToString().Contains($"PowerTube works with {power}"));
        }

        [Test]
        public void Stop_TurnOff()
        {
            int time = 60;
            int power = 50;
            _cookController.StartCooking(power, time);
            _cookController.Stop();
            Assert.That(_stringWriter.ToString().Contains("PowerTube turned off"));
        }

        [Test]
        public void OnTimerExpired_TurnOff()
        {
            int time = 60;
            int power = 50;
            _cookController.StartCooking(power, time);
            _timer.Expired += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("PowerTube turned off"));
        }
    }
}
