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
    class Step7
    {
        private IOutput _output;
        private IPowerTube _power;
        private ILight _light;
        private IDisplay _display;
        private IUserInterface _userInterface;
        private IDoor _door;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IButton _powerButton;
        private ICookController _cookController;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _power = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _cookController = Substitute.For<ICookController>();
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void OnStartCancelPressed_SetPower_TurnOff()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Light is turned off"));
        }

        [Test]
        public void OnStartCancelPressed_SetTime_TurnOn()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void OnStartCancelPressed_Cooking_TurnOff()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Light is turned off"));
        }

        [Test]
        public void OnDoorOpened_ReadyTurnOn()
        {
            _door.Opened += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void OnDoorOpened_SetPowerTurnOn()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void OnDoorOpened_SetTimeTurnOn()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void OnDoorClosed_DoorOpenTurnOff()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Light is turned off"));
        }

        [Test]
        public void CookingIsDone_TurnOff()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _userInterface.CookingIsDone();
            Assert.That(_stringWriter.ToString().Contains("Light is turned off"));
        }
    }
}
