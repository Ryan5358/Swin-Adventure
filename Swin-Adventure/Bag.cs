using System;

namespace Swin_Adventure
{
    public class Bag : Item, IHaveInventory
    {
        private Inventory _inventory;

        public Bag(string[] id, string name, string desc) :base(id, name, desc) 
        {
            _inventory = new Inventory();
        }

        public GameObject Locate(string id)
        {
            if (this.AreYou(id))
            {
                return this;
            }
            else if (Inventory.HasItem(id))
            {
                return Inventory.Fetch(id);
            }
            else
            {
                return null;
            }
        }

        public override string FullDescription
        {
            get { return $"{base.FullDescription}.\nYou look in {Name} and see{_inventory.ItemList}"; }
        }
        public Inventory Inventory
        {
            get { return _inventory; }
        }
    }
}
