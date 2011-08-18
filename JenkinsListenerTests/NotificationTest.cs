using JenkinsListener;
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

        [Test]
        public void FailingJsonPayload()
        {
            const string source =
                "{\"name\":\"billing.events\",\"url\":\"job/billing.events/\",\"build\":{\"number\":44,\"phase\":\"FINISHED\",\"status\":\"SUCCESS\",\"url\":\"job/billing.events/44/\"}}";

            var actual = Notification.New(source);

            Assert.AreEqual("billing.events", actual.Name);
            Assert.AreEqual("job/billing.events/", actual.Url);
            Assert.AreEqual(44, actual.Build.Number);
            Assert.AreEqual("FINISHED", actual.Build.Phase);
            Assert.AreEqual("SUCCESS", actual.Build.Status);
        }

    }
}
