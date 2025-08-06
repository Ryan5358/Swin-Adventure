using System;

namespace Swin_Adventure
{
    public class Inventory
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        public bool HasItem(string id)
        {
            bool isFound = false;
            foreach(Item item in _items)
            {
                if (item.AreYou(id))
                {
                    isFound = true;
                }
            }
            return isFound;
        }

        public void Put(Item item)
        {
            _items.Add(item);
        }
        public Item Take(string id)
        {
           Item takenItm = Fetch(id);
           _items.Remove(takenItm);
           return takenItm;
        }
        public Item Fetch(string id)
        {
            Item fetchedItm = null;
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    fetchedItm = item;
                }
            }
            return fetchedItm;
        }

        public string ItemList
        {
            get
            {
                string list = "";
                int count = _items.Count;
                if (count==0)
                {
                    list += " nothing.";
                }
                else
                {
                    list += ":\n";
                    for (int i = 0; i < count; i++)
                    {
                        if (i < count - 1)
                        {
                            list += $"\t{_items[i].ShortDescription}\n";
                        }
                        else
                        {
                            list += $"\t{_items[i].ShortDescription}";
                        }
                    }
                }
                return list;
            }
        }
    }
}
