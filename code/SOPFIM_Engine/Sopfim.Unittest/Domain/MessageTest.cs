using System;
using System.Collections.Generic;
using NUnit.Framework;
using SOPFIM.Domain;

namespace Sopfim.Unittest.Domain
{
    [TestFixture]
    public class MessageTest
    {
        private Message _message;
        [SetUp]
        public void Setup()
        {
            _message = new Message();
        }
        [Test]
         public void Setting_DateOuverture_will_notify_DatePrevisionIsEnabled_and_DatePrevision()
         {
             var pros = new List<string>();
             _message.PropertyChanged += (s, e) => pros.Add(e.PropertyName);
             _message.DateOuverture = DateTime.Now;
             Assert.AreEqual(4, pros.Count);
             Assert.Contains("DateOuverture", pros);
             Assert.Contains("DatePrevisionIsEnabled", pros);
             Assert.Contains("DatePrevision", pros);
             Assert.Contains("IsDirty", pros);
         }

        [Test]
        public void Setting_DateOuverture_will_set_DatePrevision_to_null()
        {
            _message.DatePrevision = DateTime.Now;
            _message.DateOuverture = DateTime.Now;
            Assert.IsFalse(_message.DatePrevision.HasValue);
        }

        [Test]
        public void SettingDateOuverture_will_disable_DatePrevision()
        {
            Assert.IsTrue(_message.DatePrevisionIsEnabled);
            _message.DateOuverture = DateTime.Now;
            Assert.IsFalse(_message.DatePrevisionIsEnabled);
        }
    }
}