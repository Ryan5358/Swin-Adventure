using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
    public class TestItem
    {
        Item T_item;

        [SetUp]
        public void Setup()
        {
            T_item = new Item(new String[] { "shovel", "spade" }, "a shovel", "This is a might fine ...");
        }

        [TestCase("shovel")]
        public void Test1_ItemIsIdentifiable(string toTest)
        {
            Assert.IsTrue(T_item.AreYou(toTest), $"Passing '{toTest}' to AreYou should return");
        }

        [TestCase("a shovel (shovel)")]
        public void Test2_ShortDescription(string toTest)
        {
            Assert.AreEqual(toTest, T_item.ShortDescription, $"Short description should show as '{toTest}'");
        }

        [TestCase("This is a might fine ...")]
        public void Test3_FullDescription(string toTest)
        {
            Assert.AreEqual(toTest, T_item.FullDescription, $"Item's description should show as '{toTest}'");
        }
    }
}
