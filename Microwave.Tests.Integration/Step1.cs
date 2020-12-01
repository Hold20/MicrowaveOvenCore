using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;
using NSubstitute;
using System.Text;



namespace Microwave.Tests.Integration
{
    public class Step1
    {
        private Output _output;
        private PowerTube _power;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _power = new PowerTube(_output);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void TestTurnOn()
        {
            _power.TurnOn(50);
            Assert.That(_stringWriter.ToString().Contains("PowerTube works with 50"));
        }

        [TestCase(110)]
        [TestCase(0)]
        public void TestTurnOnValueOutOfRange_Exception(int power)
        {

            Assert.Throws<ArgumentOutOfRangeException>(()=>_power.TurnOn(power));
        }

        [Test]
        public void TestTurnOnAlreadyOn_Exception()
        {
            _power.TurnOn(50);
            Assert.Throws<ApplicationException>(() => _power.TurnOn(50));
        }

        [Test]
        public void TestTurnOffAlreadyOn()
        {
            _power.TurnOn(100);
            _power.TurnOff();
            Assert.That(_stringWriter.ToString().Contains("PowerTube works with 100"));
        }

        [Test]
        public void TestTurnOffAlreadyOff()
        {
            _power.TurnOff();

        }

    }
}
