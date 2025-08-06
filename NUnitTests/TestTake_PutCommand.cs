using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
    public class TestTake_PutCommand
    {
        Command T_take, T_put;
        Location hallway;
        Player player;
        Item sword, shovel, gem, armour;
        Bag bag;
        string[] command;

        [SetUp]
        public void Setup()
        {
            player = new Player("Fred", "the mighty programmer");
            gem = new Item(new string[] { "gem" }, "a red gem", "A bright red ruby the size of your fist!");
            sword = new Item(new String[] { "sword", "bronze sword" }, "a bronze sword", "A short sword cast from bronze");
            shovel = new Item(new string[] { "shovel", "spade" }, "a shovel", "A metal shovel");
            armour = new Item(new string[] { "armour" }, "a set of steel armour", "A full set of armour made from steel");
            bag = new Bag(new string[] { "bag" }, "a leather bag", "A small brown leather bag");

            hallway = new Location("the Hallway", "a long well lit hallway");
            hallway.Inventory.Put(shovel);
            hallway.Inventory.Put(sword);
            bag.Inventory.Put(gem);
            player.Inventory.Put(bag);
            player.Inventory.Put(armour);

            player.Location = hallway;
            T_take = new TakeCommand();
            T_put = new PutCommand();
        }
        // ======================
        //   Take Command Tests
        // ======================

        [Test, Combinatorial]
        [Category("TakeCommand")]
        public void Test1_1__TakeItem(
            [Values("pickup", "take")] string action,
            [Values("shovel", "sword")] string item)
        {
            string name = hallway.Inventory.Fetch(item).Name;
            command = new string[] { action, item};
            Assert.That(T_take.Execute(player, command), Is.EqualTo($"You have taken {name} from the Hallway."));
            Assert.IsTrue(player.Inventory.HasItem(item));
            Assert.IsFalse(hallway.Inventory.HasItem(item));
        }

        [Test, Combinatorial]
        [Category("TakeCommand")]
        public void Test1_2__TakeNoItem(
            [Values("pickup", "take")] string action,
            [Values("gem", "pc")] string item)
        {
            command = new string[] { action, item };
            Assert.That(T_take.Execute(player, command), Is.EqualTo($"I can't find the {item}."));
            Assert.IsFalse(player.Inventory.HasItem(item));
            Assert.IsFalse(hallway.Inventory.HasItem(item));
        }

        [TestCase("pickup")]
        [TestCase("take")]
        [Category("TakeCommand")]
        public void Test1_3__TakeItemFrom(string action)
        {
            command = new string[] { action, "gem", "from", "bag" };
            Assert.That(T_take.Execute(player, command), Is.EqualTo("You have taken a red gem from a leather bag."));
            Assert.IsFalse(bag.Inventory.HasItem("gem"));
            Assert.IsTrue(player.Inventory.HasItem("gem"));
        }

        [TestCase("pickup")]
        [TestCase("take")]
        [Category("TakeCommand")]
        public void Test1_4__TakeNoItemFrom(string action)
        {
            command = new string[] { action, "book", "from", "bag" };
            Assert.That(T_take.Execute(player, command), Is.EqualTo("There is no book in a leather bag."));
            Assert.IsFalse(bag.Inventory.HasItem("book"));
            Assert.IsFalse(player.Inventory.HasItem("book"));
        }

        [TestCase("pickup")]
        [TestCase("take")]
        [Category("TakeCommand")]
        public void Test1_5__TakeGemFromNoBag(string action)
        {
            command = new string[] { action, "gem", "from", "backpack" };
            Assert.That(T_take.Execute(player, command), Is.EqualTo("I cannot find the backpack."));
            Assert.IsFalse(player.Inventory.HasItem("gem"));
        }

        [TestCase("take gem here", ExpectedResult = "I don't know how to take item like that.")]
        [TestCase("get gem", ExpectedResult = "Error in take input")]
        [TestCase("take gem at here", ExpectedResult = "Where is the item to pick up from?")]
        [Category("TakeCommand")]
        public string Test1_6_InvalidTake(string toTest)
        {
            string[] command = toTest.Split();
            return T_take.Execute(player, command);
        }
        // ======================
        //   Put Command Tests
        // ======================

        [Test, Combinatorial]
        [Category("PutCommand")]
        public void Test2_1__PutItem(
            [Values("put", "drop")] string action,
            [Values("bag", "armour")] string item)
        {
            string name = player.Inventory.Fetch(item).Name;
            command = new string[] { action, item };
            Assert.That(T_put.Execute(player, command), Is.EqualTo($"You have put {name} in the Hallway."));
            Assert.IsFalse(player.Inventory.HasItem(item));
            Assert.IsTrue(hallway.Inventory.HasItem(item));
        }

        [Test, Combinatorial]
        [Category("PutCommand")]
        public void Test2_2__PutNoItem(
            [Values("put", "drop")] string action,
            [Values("gem", "pc")] string item)
        {
            command = new string[] { action, item };
            Assert.That(T_put.Execute(player, command), Is.EqualTo($"I can't find the {item}."));
            Assert.IsFalse(player.Inventory.HasItem(item));
            Assert.IsFalse(hallway.Inventory.HasItem(item));
        }

        [TestCase("put")]
        [TestCase("drop")]
        [Category("PutCommand")]
        public void Test2_3__PutItemIn(string action)
        {
            command = new string[] { action, "armour", "in", "bag" };
            Assert.That(T_put.Execute(player, command), Is.EqualTo("You have put a set of steel armour in a leather bag."));
            Assert.IsTrue(bag.Inventory.HasItem("armour"));
            Assert.IsFalse(player.Inventory.HasItem("armour"));
        }

        [TestCase("put")]
        [TestCase("drop")]
        [Category("PutCommand")]
        public void Test2_4__PutNoItemIn(string action)
        {
            command = new string[] { action, "book", "in", "bag" };
            Assert.That(T_put.Execute(player, command), Is.EqualTo("There is no book in your inventory."));
            Assert.IsFalse(bag.Inventory.HasItem("book"));
            Assert.IsFalse(player.Inventory.HasItem("book"));
        }

        [TestCase("put")]
        [TestCase("drop")]
        [Category("PutCommand")]
        public void Test2_5__PutGemInNoBag(string action)
        {
            command = new string[] { action, "gem", "in", "backpack" };
            Assert.That(T_put.Execute(player, command), Is.EqualTo("I cannot find the backpack."));
            Assert.IsFalse(player.Inventory.HasItem("gem"));
        }

        [TestCase("drop gem here", ExpectedResult = "I don't know how to put item like that.")]
        [TestCase("remove gem", ExpectedResult = "Error in put input")]
        [TestCase("drop gem at here", ExpectedResult = "Where to put the item in?")]
        [Category("PutCommand")]
        public string Test2_6_InvalidPut(string toTest)
        {
            string[] command = toTest.Split();
            return T_put.Execute(player, command);
        }
    }
}
