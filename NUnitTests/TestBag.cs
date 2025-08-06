using NUnit.Framework;
using Swin_Adventure;


namespace NUnitTests
{
    public class TestBag
    {
        Item[] myItems;
        Item gem;
        Bag T_bag1 ,T_bag2;

        [SetUp]
        public void Setup()
        {
            myItems = new Item[2];
            myItems[0] = new Item(new String[] { "sword", "bronze sword" }, "a bronze sword", "A short sword cast from bronze");
            myItems[1] = new Item(new String[] { "shovel", "spade" }, "a shovel", "A metal shovel");
            gem = new Item(new string[] { "gem" }, "a red gem", "A bright red ruby the size of your fist!");
            T_bag2 = new Bag(new string[] { "b2" }, "the leather bag", "A small brown leather bag");
            foreach (Item item in myItems)
            {
                T_bag2.Inventory.Put(item);
            }
            T_bag1 = new Bag(new string[] { "b1" }, "a backpack", "A brown leather medium sized backpack");
        }

        [Test]
        public void Test1_BagLocatesItems()
        {
            Assert.IsTrue(T_bag2.Locate("sword") == myItems[0] & T_bag2.Inventory.HasItem("sword"));
        }

        [Test]
        public void Test2_BagLocatesItself()
        {
            Assert.That(T_bag2.Locate("b2"), Is.EqualTo(T_bag2));
        }

        [Test]
        public void Test3_BagLocatesNothing()
        {
            Assert.That(T_bag2.Locate("pc"), Is.EqualTo(null));
        }

        [Test]
        public void Test4_BagFullDescription()
        {
            Assert.That(T_bag2.FullDescription, Is.EqualTo("A small brown leather bag.\nYou look in the leather bag and see:\n\ta bronze sword (sword)\n\ta shovel (shovel)"));
        }

        [Test]
        public void Test5_1_BagInBag_b1Locatesb2()
        {
            T_bag1.Inventory.Put(T_bag2);
            Assert.That(T_bag1.Locate("b2"), Is.EqualTo(T_bag2));
        }
        [Test]
        public void Test5_2_BagInBag_b1LocatesItemsInb2()
        {
            T_bag1.Inventory.Put(T_bag2);
            T_bag1.Inventory.Put(gem);
            Assert.That(T_bag1.Locate("gem"), Is.EqualTo(gem));
        }
        [Test]
        public void Test5_3_BagInBag_b1NotLocatesItemsInb2()
        {
            T_bag1.Inventory.Put(T_bag2);
            Assert.That(T_bag1.Locate("sword"), Is.EqualTo(null));
            Assert.That(T_bag1.Locate("shovel"), Is.EqualTo(null));
        }
    }
}
