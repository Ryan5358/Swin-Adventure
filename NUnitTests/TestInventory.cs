using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
    public class TestInventory
    {
        Item[] myItems;
        Inventory T_inventory;

        [SetUp]
        public void Setup()
        {
            myItems = new Item[2];
            myItems[0] = new Item(new String[] { "sword", "bronze sword" }, "a bronze sword", "A short sword cast from bronze");
            myItems[1] = new Item(new String[] { "pc", " computer" }, "a small computer", "The light from the monitor of this computer illuminates the room");
            T_inventory = new Inventory();
            foreach(Item item in myItems)
            {
                T_inventory.Put(item);
            }
        }

        [Test]
        public void Test1_FindItem()
        {
            Assert.IsTrue(T_inventory.HasItem("sword"));
        }

        [Test]
        public void Test2_NoItemFind()
        {
            Assert.IsFalse(T_inventory.HasItem("shovel"));
        }

        [Test]
        public void Test3_FetchItem()
        {
            Assert.IsTrue(T_inventory.Fetch("sword") == myItems[0] & T_inventory.HasItem("sword"));
        }

        [Test]
        public void Test4_TakeItem()
        {
            Assert.IsTrue(T_inventory.Take("sword") == myItems[0] & !T_inventory.HasItem("sword"));
        }

        [Test]
        public void Test5_ItemList()
        {
            Assert.AreEqual(":\n\ta bronze sword (sword)\n\ta small computer (pc)", T_inventory.ItemList);
        }
    }
}