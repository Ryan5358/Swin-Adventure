using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
    public class TestLocation
    {
        Location T_Hallway;
        Player player;
        Item sword, shovel;
        Command LCommand;

        [SetUp]
        public void Setup()
        {
            sword = new Item(new string[] { "sword", "bronze sword" }, "a bronze sword", "A short sword cast from bronze");
            shovel = new Item(new string[] { "shovel", "spade" }, "a shovel", "A metal shovel");

            T_Hallway = new Location("the Hallway", "This is a long well lit hallway");
            T_Hallway.Inventory.Put(shovel);
            T_Hallway.Inventory.Put(sword);

            player = new Player("Fred", "the mighty programmer");
            player.Location = T_Hallway;

            LCommand = new LookCommand();
        }

        [TestCase("here")]
        [TestCase("room")]
        public void Test1_LocationIsIdentifiable(string idents)
        {
            Assert.IsTrue(T_Hallway.AreYou(idents));
        }

        [TestCase("shovel")]
        [TestCase("sword")]
        public void Test2_LocationLocatesItemItHas(string itemId)
        {
            Assert.That(T_Hallway.Locate(itemId),Is.Not.Null);
        }

        [TestCase("shovel")]
        [TestCase("sword")]
        public void Test3_PlayerLocatesItemsInLocation(string itemId)
        {
            Assert.That(player.Locate(itemId), Is.Not.Null);
        }

        [Test]
        public void Test4_LCommandLook()
        {
            Assert.That(LCommand.Execute(player, new string[] { "look" }), Is.EqualTo(T_Hallway.FullDescription));
        }
    }
}
