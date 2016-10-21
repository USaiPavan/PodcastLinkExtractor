using System;
using System.Linq;
using NUnit.Framework;
using PodcastLinkExtractor.Infrastructure;


namespace PodcastLinkExtractor.Tests
{
    [TestFixture]
    public class ApplicationTests
    {
        [Test]
        public void ApplicationContainerMustBeNullBeforeCallingRegisterDependencies()
        {
            Assert.IsNull(Application.CurrentContainer);
        }

        [Test]
        public void ApplicationContainerMustNotBeNullAfterCallingRegisterDependencies()
        {
            Application.RegisterDependencies();
            Assert.IsNotNull(Application.CurrentContainer);
            Application.Reset();
        }

        [Test]
        public void ApplicationContainerMustBeASpecificValueAfterCallingRegisterDependencies()
        {   
            Application.RegisterDependencies();
            Assert.AreEqual(3, Application.CurrentContainer.Registrations.Count());
            Application.Reset();
        }
    }
}
