using NUnit.Framework;

namespace PetBoardingTests
{
    [TestFixture]
    public class BaseTests
    {
        [Test]
        public void BaseTest()
        {
            Assert.That(1 == 1, "Pass condition");
        }
    }
}