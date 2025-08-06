using System;

namespace Swin_Adventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private Location _location;

        public Player(string name, string desc) : base (new string[] {"me", "inventory", "inv"}, name, desc)
        {
            _inventory = new Inventory();
            _location = null;
        }

        public GameObject Locate(string id)
        {
            if (this.AreYou(id))
            {
                return this;
            }
            else if(_inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            }
            else if(_location != null)
            {
                return _location.Locate(id);  
            }
            else
            {
                return null;
            }
        }

        public override string FullDescription
        {
            get
            {
                return $"You are {Name}, {base.FullDescription}.\nYou are carrying{_inventory.ItemList}";
            }
        }
        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}