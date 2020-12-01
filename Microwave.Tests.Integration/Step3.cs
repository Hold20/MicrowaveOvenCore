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

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _light = new Light(_output);
        }

        [Test]
        public void TurnOnLight()
        {

        }

    }

    
     
}
