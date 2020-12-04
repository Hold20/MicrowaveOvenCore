using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Tests.Integration
{
    class Step5
    {
        private IOutput _output;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ICookController _cookController;
        private ITimer _timer;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _timer = Substitute.For<ITimer>();
            _cookController = new CookController(_timer, _display, _powerTube);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void OnTimerTick_Display()
        {
            int power = 50;
            int time = 120;
            _cookController.StartCooking(power, time);
            _timer.TimeRemaining.Returns(120);
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            Assert.That(_stringWriter.ToString().Contains("Display shows: 02:00"));
        }
    }
}

