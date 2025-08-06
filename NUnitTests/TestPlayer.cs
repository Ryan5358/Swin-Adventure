using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
    public class TestPlayer
    {
        Player T_player;
        Inventory myInventory;
        Item[] myItems;

        [SetUp]
        public void Setup()
        {
            T_player = new Player("Fred", "the mighty programmer");
            myItems = new Item[2];
            myItems[0] = new Item(new String[] { "sword", "bronze sword" }, "a bronze sword", "A short sword cast from bronze");
            myItems[1] = new Item(new String[] { "pc", " computer" }, "a small computer", "The light from the monitor of this computer illuminates the room");
            myInventory = T_player.Inventory;
            foreach (Item item in myItems)
            {
                myInventory.Put(item);
            }
        }


        [TestCase("me")]
        [TestCase("inventory")]
        public void Test1_PlayerIsIdentifiable(string toTest)
        {
            Assert.IsTrue(T_player.AreYou(toTest), $"Passing '{toTest}' to AreYou should return");
        }

        [Test]
        public void Test2_PlayerLocatesItems()
        {
            Assert.That(T_player.Locate("sword"), Is.EqualTo(myItems[0]));
        }

        [TestCase("me")]
        [TestCase("inventory")]
        public void Test3_PlayerLocatesItself(string toTest)
        {
            Assert.That(T_player.Locate(toTest), Is.EqualTo(T_player));
        }

        [Test]
        public void Test4_PlayerLocatesNothing()
        {
            Assert.That(T_player.Locate("shovel"), Is.EqualTo(null));
        }

        [Test]
        public void Test5_PlayerFullDescription()
        {
            Assert.AreEqual($"You are Fred, the mighty programmer.\nYou are carrying:\n\ta bronze sword (sword)\n\ta small computer (pc)", T_player.FullDescription);
        }
    }
}
