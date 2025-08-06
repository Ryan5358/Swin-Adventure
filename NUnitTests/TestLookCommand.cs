using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
    public class TestLookCommand
    {
        Command T_look;
        Player player;
        Item gem, sword;
        Bag bag;

        [SetUp]
        public void Setup()
        {
            player = new Player("Fred", "the mighty programmer");
            gem = new Item(new string[] { "gem" }, "a red gem", "A bright red ruby the size of your fist!");
            sword = new Item(new String[] { "sword", "bronze sword" }, "a bronze sword", "A short sword cast from bronze");
            bag = new Bag(new string[] { "bag" }, "a leather bag", "A small brown leather bag");
            T_look = new LookCommand();
            player.Inventory.Put(sword);
        }

        [Test]
        public void Test1_LookAtMe()
        {
            string[] command = { "look", "at", "inventory" };
            Assert.That(T_look.Execute(player, command), Is.EqualTo(player.FullDescription));
        }
        [Test]
        public void Test2_LookAtGem()
        {
            player.Inventory.Put(gem);
            string[] command = { "look", "at", "gem" };
            Assert.That(T_look.Execute(player, command), Is.EqualTo(gem.FullDescription));
        }
        [Test]
        public void Test3_LookAtUnk()
        {
            string[] command = { "look", "at", "gem" };
            Assert.That(T_look.Execute(player, command), Is.EqualTo("I can't find the gem."));
        }
        [Test]
        public void Test4_LookAtGemInMe()
        {
            player.Inventory.Put(gem);
            string[] command = { "look", "at", "gem", "in", "me" };
            Assert.That(T_look.Execute(player, command), Is.EqualTo(gem.FullDescription));
        }
        [Test]
        public void Test5_LookAtGemInBag()
        {
            player.Inventory.Put(bag);
            bag.Inventory.Put(gem);
            string[] command = { "look", "at", "gem", "in", "bag" };
            Assert.That(T_look.Execute(player, command), Is.EqualTo(gem.FullDescription));
        }
        [Test]
        public void Test6_LookAtGemInNoBag()
        {
            bag.Inventory.Put(gem);
            string[] command = { "look", "at", "gem", "in", "bag" };
            Assert.That(T_look.Execute(player, command), Is.EqualTo("I cannot find the bag."));
        }
        [Test]
        public void Test7_LookAtNoGemInBag()
        {
            player.Inventory.Put(bag);
            string[] command = { "look", "at", "gem", "in", "bag" };
            Assert.That(T_look.Execute(player, command), Is.EqualTo("I cannot find the gem in a leather bag."));
        }

        [TestCase("see gem", ExpectedResult = "I don't know how to look like that.")]
        [TestCase("peek in bag", ExpectedResult = "Error in look input")]
        [TestCase("look for gem", ExpectedResult = "What do you want to look at?")]
        [TestCase("look at gem at bag", ExpectedResult = "What do you want to look in?")]
        public string Test8_InvalidLook(string toTest)
        {
            string[] command = toTest.Split();
            return T_look.Execute(player, command);
        }
    }
}
