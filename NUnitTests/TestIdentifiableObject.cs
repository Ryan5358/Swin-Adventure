using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
    public class TestIdentifiableObject
    {
        IdentifiableObject _id;

        [SetUp]
        public void Setup()
        {
            _id = new IdentifiableObject(new string[] { "fred", "bob" });
        }

        [TestCase("fred")]
        [TestCase("bob")]
        public void TestAreYou(string toTest)
        {
            Assert.IsTrue(_id.AreYou(toTest), $"Passing '{toTest}' to AreYou should return");
        }

        [TestCase("wilma")]
        [TestCase("boby")]
        public void TestNotAreYou(string toTest)
        {
            Assert.IsFalse(_id.AreYou(toTest), $"Passing '{toTest}' to AreYou should return");
        }

        [TestCase("FRED")]
        [TestCase("bOB")]
        public void TestCaseSensitive(string toTest)
        {
            Assert.IsTrue(_id.AreYou(toTest), $"Passing '{toTest}' to AreYou should return");
        }

        [TestCase("fred")]
        public void TestFirstId(string toTest)
        {
            Assert.AreEqual(toTest, _id.FirstId, $"Passing '{toTest}' to FirstId should return");
        }

        [TestCase("")]
        public void TestFirstIdWithNoIds(string toTest)
        {
            IdentifiableObject _emptyIds = new IdentifiableObject(new string[] { });
            Assert.AreEqual(toTest, _emptyIds.FirstId, $"Passing '{toTest}' to FirstId should return");
        }

        [TestCase("wilma")]
        public void TestAddId(string toTest)
        {
            _id.AddIdentifier(toTest);
            Assert.IsTrue(_id.AreYou(toTest), $"Passing '{toTest}' to AreYou should return");
        }

    }
}