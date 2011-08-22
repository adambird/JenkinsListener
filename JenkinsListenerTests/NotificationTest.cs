using JenkinsListener;
using NUnit.Framework;

namespace JenkinsListenerTests
{
    [TestFixture]
    public class NotificationTestNew
    {
        private Notification _actual;

        [SetUp]
        public void GivenJsonStringWhenCreatingNewNotification()
        {
            const string source = "{\"name\":\"JobName\",\"url\":\"JobUrl\",\"build\":{\"number\":1,\"phase\":\"STARTED\",\"status\":\"FAILED\"}}";
            _actual = Notification.New(source);
        }

        [Test]
        public void ThenTheJobNameIsSet()
        {
            Assert.AreEqual("JobName", _actual.Name);
        }

        [Test]
        public void ThenTheJobUrlIsSet()
        {
            Assert.AreEqual("JobUrl", _actual.Url);
        }

        [Test]
        public void ThenTheBuildNumberIsSet()
        {
            Assert.AreEqual(1, _actual.Build.Number);
        }

        [Test]
        public void ThenTheBuildPhaseIsSet()
        {
            Assert.AreEqual("STARTED", _actual.Build.Phase);
        }

        [Test]
        public void ThenTheBuildStatusIsSet()
        {
            Assert.AreEqual("FAILED", _actual.Build.Status);
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

    [TestFixture]
    public class NotificationTestGetFilePath
    {
        private string _returnedContents;

        [SetUp]
        public void GivenAConfiguredScriptFileDirectoryPathWhenGettingFileContents()
        {
            _returnedContents = Notification.GetScriptFileContentForJobUrl("joburl");
        }

        [Test]
        public void ThenTheFileContentReturnedIsCorrect()
        {
            Assert.That(_returnedContents, Is.EqualTo("hello world"));
        }
    }
}
