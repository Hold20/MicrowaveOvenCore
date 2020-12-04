using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace Microwave.Tests.Integration
{
    class Step2
    {
        private IOutput _output;
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _display = new Display(_output);
        }

        [TestCase(11,11)]
        public void ShowTime(int min, int sec)
        {
            _display.ShowTime(min, sec);
        }

        [TestCase(50)]
        public void ShowPower(int power)
        {
            _display.ShowPower(power);
        }

        [Test]
        public void Clear()
        {
            _display.Clear();
        }
    }
}

