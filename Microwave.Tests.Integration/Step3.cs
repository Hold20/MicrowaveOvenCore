using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace Microwave.Tests.Integration
{
    class Step3
    {
        private IOutput _output;
        private ILight _light;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _light = new Light(_output);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void TurnOnLight_WhenWasOff()
        {
            _light.TurnOn();
            Assert.That(_stringWriter.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void TurnOffLight_WhenWasOn()
        {
            _light.TurnOn();
            _light.TurnOff();
            Assert.That(_stringWriter.ToString().Contains("Light is turned off"));
        }

    }

}
