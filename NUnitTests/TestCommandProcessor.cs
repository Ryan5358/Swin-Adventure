using NUnit.Framework;
using Swin_Adventure;
using Path = Swin_Adventure.Path;

namespace NUnitTests
{
    public class TestCommandProcessor
    {
        CommandProcessor T_cp;
        Player player;
        Location hallway, small_closet;
        Path path1, path2;
        Item sword, shovel, gem, armour;
        Bag bag;
        string command;

        [SetUp]
        public void SetUp()
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
            small_closet = new Location("a small Closet", "a small dark closet, with an odd smell");
            path1 = new Path(new string[] { "south", "s" }, "South", "go through a door", small_closet);
            path2 = new Path(new string[] { "north", "n" }, "North", "go through a door", hallway);

            hallway.SetPaths(new Path[] { path1 });
            small_closet.SetPaths(new Path[] { path2 });
            bag.Inventory.Put(gem);
            player.Inventory.Put(bag);
            player.Inventory.Put(sword);
            player.Location = hallway;
            player.Inventory.Put(armour);

            T_cp = new CommandProcessor(new Command[] 
                { 
                    new LookCommand(), 
                    new MoveCommand(), 
                    new TakeCommand(), 
                    new PutCommand() 
                });
        }

        // ======================
        //   Look Command Tests
        // ======================

        [Test]
        [Category("LookCommand")]
        public void Test1_1__LookAtLocation()
        {
            command = "look";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo(hallway.FullDescription));
        }

        [Test]
        [Category("LookCommand")]
        public void Test1_2__LookAtMe()
        {
            command = "look at me";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo(player.FullDescription));
        }

        [Test]
        [Category("LookCommand")]
        public void Test1_3__LookAtGem()
        {
            player.Inventory.Put(gem);
            command = "look at gem";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo(gem.FullDescription));
        }

        [Test]
        [Category("LookCommand")]
        public void Test1_4__LookAtUnk()
        {
            command = "look at gem";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("I can't find the gem."));
        }

        [Test]
        [Category("LookCommand")]
        public void Test1_5__LookAtGemInMe()
        {
            player.Inventory.Put(gem);
            command = "look at gem in me";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo(gem.FullDescription));
        }

        [Test]
        [Category("LookCommand")]
        public void Test1_6__LookAtGemInBag()
        {
            player.Inventory.Put(bag);
            bag.Inventory.Put(gem);
            command = "look at gem in bag";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo(gem.FullDescription));
        }

        [Test]
        [Category("LookCommand")]
        public void Test1_7__LookAtGemInNoBag()
        {
            player.Inventory.Take("bag");
            bag.Inventory.Put(gem);
            command = "look at gem in bag";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("I cannot find the bag."));
        }

        [Test]
        [Category("LookCommand")]
        public void Test1_8__LookAtNoGemInBag()
        {
            bag.Inventory.Take("gem");
            player.Inventory.Put(bag);
            command = "look at gem in bag";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("I cannot find the gem in a leather bag."));
        }

        [TestCase("see gem", ExpectedResult = "I don't understand see gem.")]
        [TestCase("peek in bag", ExpectedResult = "I don't understand peek in bag.")]
        [TestCase("look for gem", ExpectedResult = "What do you want to look at?")]
        [TestCase("look at gem at bag", ExpectedResult = "What do you want to look in?")]
        [TestCase("look gem", ExpectedResult = "I don't know how to look like that.")]
        [Category("LookCommand")]
        public string Test1_9__InvalidLook(string command)
        {
            return T_cp.Execute(player, command);
        }
        // ======================
        //   Move Command Tests
        // ======================

        [Test, Combinatorial]
        [Category("MoveCommand")]
        public void Test2_1__Player_LeavesLocation_OnValidPath(
            [Values("move ", "go ", "leave ", "head ")] string action,
            [Values("south", "s")] string direction)
        {
            command = action + direction;
            Assert.That(T_cp.Execute(player, command), Is.EqualTo(path1.FullDescription));
            Assert.That(player.Location, Is.EqualTo(small_closet));
        }

        [Test, Combinatorial]
        [Category("MoveCommand")]
        public void Test2_2__Player_RemainsInLocation_OnInvalidPath(
            [Values("move ", "go ", "leave ", "head ")] string action,
            [Values("east", "e")] string direction)
        {
            command = action + direction;
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("I can't move there."));
            Assert.That(player.Location, Is.EqualTo(hallway));
        }

        [TestCase("go to the east", ExpectedResult = "I don't know how to move like that.")]
        [TestCase("travel east", ExpectedResult = "I don't understand travel east.")]
        [Category("MoveCommand")]
        public string Test2_3__InvalidMove(string command)
        {
            return T_cp.Execute(player, command);
        }

        // ======================
        //   Take Command Tests
        // ======================

        [Test, Combinatorial]
        [Category("TakeCommand")]
        public void Test3_1__TakeItem(
            [Values("pickup ", "take ")] string action,
            [Values("shovel", "sword")] string item)
        {
            string name = hallway.Inventory.Fetch(item).Name;
            command = action + item;
            Assert.That(T_cp.Execute(player, command), Is.EqualTo($"You have taken {name} from the Hallway."));
            Assert.IsTrue(player.Inventory.HasItem(item));
            Assert.IsFalse(hallway.Inventory.HasItem(item));
        }

        [Test, Combinatorial]
        [Category("TakeCommand")]
        public void Test3_2__TakeNoItem(
            [Values("pickup ", "take ")] string action,
            [Values("gem", "pc")] string item)
        {
            command = action + item;
            Assert.That(T_cp.Execute(player, command), Is.EqualTo($"I can't find the {item}."));
            Assert.IsFalse(player.Inventory.HasItem(item));
            Assert.IsFalse(hallway.Inventory.HasItem(item));
        }

        [TestCase("pickup ")]
        [TestCase("take ")]
        [Category("TakeCommand")]
        public void Test3_3__TakeItemFrom(string action)
        {
            command = action + "gem from bag";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("You have taken a red gem from a leather bag."));
            Assert.IsFalse(bag.Inventory.HasItem("gem"));
            Assert.IsTrue(player.Inventory.HasItem("gem"));
        }

        [TestCase("pickup ")]
        [TestCase("take ")]
        [Category("TakeCommand")]
        public void Test3_4__TakeNoItemFrom(string action)
        {
            command = action + "book from bag";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("There is no book in a leather bag."));
            Assert.IsFalse(bag.Inventory.HasItem("book"));
            Assert.IsFalse(player.Inventory.HasItem("book"));
        }

        [TestCase("pickup ")]
        [TestCase("take ")]
        [Category("TakeCommand")]
        public void Test3_5__TakeGemFromNoBag(string action)
        {
            command = action + "gem from backpack";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("I cannot find the backpack."));
            Assert.IsFalse(player.Inventory.HasItem("gem"));
        }

        [TestCase("take gem here", ExpectedResult = "I don't know how to take item like that.")]
        [TestCase("get gem", ExpectedResult = "I don't understand get gem.")]
        [TestCase("take gem at here", ExpectedResult = "Where is the item to pick up from?")]
        [Category("TakeCommand")]
        public string Test3_6_InvalidTake(string command)
        {
            return T_cp.Execute(player, command);
        }
        // ======================
        //   Put Command Tests
        // ======================

        [Test, Combinatorial]
        [Category("PutCommand")]
        public void Test4_1__PutItem(
            [Values("put " , "drop ")] string action,
            [Values("bag", "armour")] string item)
        {
            string name = player.Inventory.Fetch(item).Name;
            command = action + item;
            Assert.That(T_cp.Execute(player, command), Is.EqualTo($"You have put {name} in the Hallway."));
            Assert.IsFalse(player.Inventory.HasItem(item));
            Assert.IsTrue(hallway.Inventory.HasItem(item));
        }

        [Test, Combinatorial]
        [Category("PutCommand")]
        public void Test4_2__PutNoItem(
            [Values("put " , "drop ")] string action,
            [Values("gem", "pc")] string item)
        {
            command = action + item;
            Assert.That(T_cp.Execute(player, command), Is.EqualTo($"I can't find the {item}."));
            Assert.IsFalse(player.Inventory.HasItem(item));
            Assert.IsFalse(hallway.Inventory.HasItem(item));
        }

        [TestCase("put " )]
        [TestCase("drop ")]
        [Category("PutCommand")]
        public void Test4_3__PutItemIn(string action)
        {
            command = action + "sword in bag";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("You have put a bronze sword in a leather bag."));
            Assert.IsTrue(bag.Inventory.HasItem("sword"));
            Assert.IsFalse(player.Inventory.HasItem("sword"));
        }

        [TestCase("put " )]
        [TestCase("drop ")]
        [Category("PutCommand")]
        public void Test4_4__PutNoItemIn(string action)
        {
            command = action + "book in bag";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("There is no book in your inventory."));
            Assert.IsFalse(bag.Inventory.HasItem("book"));
            Assert.IsFalse(player.Inventory.HasItem("book"));
        }

        [TestCase("put " )]
        [TestCase("drop ")]
        [Category("PutCommand")]
        public void Test4_5__PutGemInNoBag(string action)
        {
            command = action + "gem in backpack";
            Assert.That(T_cp.Execute(player, command), Is.EqualTo("I cannot find the backpack."));
            Assert.IsFalse(player.Inventory.HasItem("gem"));
        }

        [TestCase("drop gem here", ExpectedResult = "I don't know how to put item like that.")]
        [TestCase("remove gem", ExpectedResult = "I don't understand remove gem.")]
        [TestCase("drop gem at here", ExpectedResult = "Where to put the item in?")]
        [Category("PutCommand")]
        public string Test4_6_InvalidPut(string command)
        {
            return T_cp.Execute(player, command);
        }

    }
}
