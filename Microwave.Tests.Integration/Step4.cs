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

        [Test]
        public void StartCooking_WithValidParameters()
        {
            _cookController.StartCooking(50,60);
            Assert.That(_stringWriter.ToString().Contains("PowerTube works with 50"));
        }

        [Test]
        public void StopCooking()
        {
            _cookController.StartCooking(50,60);
            _cookController.Stop();
            Assert.That(_stringWriter.ToString().Contains("Powertube turned off"));
        }




    }
}
