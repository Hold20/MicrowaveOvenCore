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
    class Step8
    {
        private IOutput _output;
        private IPowerTube _power;
        private IDisplay _display;
        private ILight _light;
        private ITimer _timer;
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IUserInterface _userInterface;
        private ICookController _cookController;
        private StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _power = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = Substitute.For<ITimer>();
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _cookController = new CookController(_timer, _display, _power);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void OnStartCancelPressed_SetTimeTurnOff()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("PowerTube works with"));
        }

        [Test]
        public void OnStartCancelPressed_SetTime_MorePower()
        {
            for (int i = 0; i < 10; i++)
            {
                _powerButton.Pressed += Raise.Event();
            }

            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("PowerTube works with"));
        }

        [Test]
        public void OnStartCancelPressed_Cooking_TurnOff()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("PowerTube turned off"));
        }

        [Test]
        public void OnDoorOpened_Cooking()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("PowerTube turned off"));
        }
    }
}
