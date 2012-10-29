using System;
using NUnit.Framework;
using Sopfim.CustomControls;

namespace Sopfim.Unittest.Converters
{
    [TestFixture]
    public class AmPmConverterTest
    {
        private AmPmConverter _multiConverter;

        [SetUp]
        public void Setup()
        {
            _multiConverter = new AmPmConverter();
        }

        


        [Test]
        public void Should_return_am_when_null()
        {
            var date = null as DateTime?;
            var result = _multiConverter.Convert(date, typeof(string), null, null);
            Assert.AreEqual("AM", (string) result);
        }

        [Test]
        public void Should_return_am_when_morning_date()
        {
            var date = new DateTime(2002, 10, 10, 5, 0, 0);
            var result = _multiConverter.Convert(date, typeof(string), null, null);
            Assert.AreEqual("AM", (string)result);
        }

        [Test]
        public void Should_return_pm_when_evening_date()
        {
            var date = new DateTime(2002, 10, 10, 17, 0, 0);
            var result = _multiConverter.Convert(date, typeof(string), null, null);
            Assert.AreEqual("PM", (string)result);
        }
    }
}