using System;

namespace Swin_Adventure
{
    public class Location : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private List<Path> _paths;

        public Location(string name, string desc) : base(new string[] { "here", "room" }, name, desc) 
        {
            _inventory = new Inventory();
            _paths = new List<Path>();
        }

        public GameObject Locate(string id)
        {
            if(AreYou(id))
            {
                return this;
            }
            if (_inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            }
            return null;
        }
        public void SetPaths(Path[] exits)
        {
            foreach(Path exit in exits)
            {
                _paths.Add(exit);
            }
        }
        public void ClosePaths(Path[] exits)
        {
            foreach( Path exit in exits)
            {
                _paths.Remove(exit);
            }
        }
        public Path FetchPath(string id)
        {
            Path fetchedPath = null;
            foreach (Path path in _paths)
            {
                if(path.AreYou(id))
                {
                    fetchedPath = path;
                }
            }
            return fetchedPath;
        }

        public override string FullDescription
        {
            get 
            {
                string localItems = "In this room you can see";
                if (Inventory.ItemList != "")
                {
                    localItems += $"{Inventory.ItemList}";
                }
                else
                {
                    localItems += " nothing.";
                }
                string exitText = "There";
                if (_paths.Count > 0)
                {
                    int count = _paths.Count;
                    if (count == 1)
                    {
                        exitText += $" is exit to the {_paths[0].Name.ToLower()}";
                    }
                    else
                    {
                        exitText += " are exits to the";
                        for (int i = 0; i < count; i++)
                        {                     
                            if (i < count - 1)
                            {
                                exitText += $" {_paths[i].Name.ToLower()},";
                            }
                            else
                            {
                                exitText += $" and {_paths[i].Name.ToLower()}";
                            }                        
                        }
                    }
                }
                else
                {
                    exitText += " are no exits.";
                }
                return $"You are in {Name}.\n{base.FullDescription}.\n{exitText}.\n{localItems}"; 
            }
        }
        public Inventory Inventory
        { 
            get { return _inventory; } 
        }
        public List<Path> Paths
        {
            get { return _paths; }
        }
    }
}
