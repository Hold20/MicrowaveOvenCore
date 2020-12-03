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
        private IPowerTube _power;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cookController;
        private ITimer _timer;
        private IUserInterface _userInterface;
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private StringWriter _stringWriter;


        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _power = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _power);
            _timer = Substitute.For<ITimer>();
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void CookingIsDone_ClearDisplay()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            while (_timer.TimeRemaining != 0)
            {

            }

            Assert.That(_stringWriter.ToString().Contains("Display cleared"));
        }


    }
}
