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
    class Step6
    {
        private IOutput _output;
        private IPowerTube _powerTube;
        private Display _display;
        private IUserInterface _userInterface;
        private ICookController _cookController;
        private ILight _light;
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void OnPowerPressed_Display()
        {
            _powerButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("50"));
        }

        [Test]
        public void OnPowerPressed_5_Times()
        {
            for (int i = 0; i < 5; i++)
            {
                _powerButton.Pressed += Raise.Event();
            }

            Assert.That(_stringWriter.ToString().Contains("250"));
        }

        [Test]
        public void OnPowerPressed_15_Times_Reset()
        {
            for (int i = 0; i < 15; i++)
            {
                _powerButton.Pressed += Raise.Event();
            }

            Assert.That(_stringWriter.ToString().Contains("50"));
        }

        [Test]
        public void OnTimePressed_Display()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("1"));
        }

        [Test]
        public void OnTimePressed_10_Times()
        {
            _powerButton.Pressed += Raise.Event();
            for (int i = 0; i < 10; i++)
            {
                _timeButton.Pressed += Raise.Event();
            }

            Assert.That(_stringWriter.ToString().Contains("10"));
        }

        [Test]
        public void OnStartCancelButton_Pressed_Before()
        {
            _powerButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Display cleared"));
        }

        [Test] //VIRKER IKKE
        public void OnStartCancelButton_Pressed_After()
        {
            _powerButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Display cleared"));
        }

        [Test]
        public void OnDoorOpen_Ready()
        {
            _door.Opened += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains(""));
        }

        [Test]
        public void OnDoorOpen_SetPower()
        {
            _powerButton.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Display cleared"));
        }

        [Test]
        public void OnDoorOpen_SetTime()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Display cleared"));
        }

        [Test]
        public void OnDoorOpen_Cooking()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            Assert.That(_stringWriter.ToString().Contains("Display cleared"));
        }

        [Test]
        public void TimerExpired_Display()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _userInterface.CookingIsDone();
            Assert.That(_stringWriter.ToString().Contains("Display cleared"));
        }
    }
}
