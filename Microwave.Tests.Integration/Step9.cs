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
    class Step9
    {
        private IOutput _output;
        private IDisplay _display;
        private ILight _light;
        private IPowerTube _power;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IUserInterface _userInterface;
        private ICookController _cookController;
        private ITimer _timer;
        private StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _power = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _powerButton = new Button();
            _door = Substitute.For<IDoor>();
            _timer = Substitute.For<ITimer>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light,
                _cookController);
            _cookController = new CookController(_timer, _display, _power);
            _cookController.UI = _userInterface;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void PowerButton_1_Time()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Press();
            Assert.That(_stringWriter.ToString().Contains("50 W"));
        }

        [Test]
        public void PowerButton_7_Time()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            for (int i = 0; i < 7; i++)
            {
                _powerButton.Press();
            }

            Assert.That(_stringWriter.ToString().Contains("350 W"));
        }

        [Test]
        public void PowerButton_15_Time()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            for (int i = 0; i < 15; i++)
            {
                _powerButton.Press();
            }

            Assert.That(_stringWriter.ToString().Contains("50 W"));
        }

    }
}
