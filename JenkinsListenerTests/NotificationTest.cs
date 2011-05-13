using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JenkinsListenerService;
using NUnit.Framework;

namespace JenkinsListenerTests
{
    [TestFixture]
    public class NotificationTest
    {
        [Test]
        public void NewFromJson()
        {
            const string source = "{\"name\":\"JobName\",\"url\":\"JobUrl\",\"build\":{\"number\":1,\"phase\":\"STARTED\",\"status\":\"FAILED\"}}";

            var actual = Notification.New(source);

            Assert.AreEqual("JobName", actual.Name);
            Assert.AreEqual("JobUrl", actual.Url);
            Assert.AreEqual(1, actual.Build.Number);
            Assert.AreEqual("STARTED", actual.Build.Phase);
            Assert.AreEqual("FAILED", actual.Build.Status);
        }

    }
}
