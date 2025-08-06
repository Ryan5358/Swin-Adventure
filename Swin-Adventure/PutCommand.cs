using System;

namespace Swin_Adventure
{
    public class PutCommand : Command
    {
        public PutCommand() : base(new string[] { "put", "drop" }) { }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 2 & text.Length != 4)
            {
                return "I don't know how to put item like that.";
            }
            else
            {
                if (!AreYou(text[0].ToLower()))
                {
                    return "Error in put input";
                }
                else
                {
                    Item item = p.Inventory.Fetch(text[1]) as Item;
                    if (text.Length == 2)
                    {
                        
                        if (item == null)
                        {
                            return $"I can't find the {text[1]}.";
                        }
                        else
                        {
                            p.Location.Inventory.Put(item);
                            p.Inventory.Take(text[1]);
                            return $"You have put {item.Name} in {p.Location.Name}.";
                        }
                    }
                    else
                    {
                        if (text[2].ToLower() != "in")
                        {
                            return "Where to put the item in?";
                        }
                        else
                        {
                            IHaveInventory container = FetchContainer(p, text[3]);
                            if (container == null)
                            {
                                return $"I cannot find the {text[3]}.";
                            }
                            else
                            {
                                return PutIn(p, text[1], container);
                            }
                        }
                    }
                }
            }
        }
        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            if (p.Locate(containerId) != null)
            {
                return p.Locate(containerId) as IHaveInventory;
            }
            return null;
        }
        private string PutIn(Player p, string thingId, IHaveInventory container)
        {
            Item item = p.Inventory.Fetch(thingId) as Item;
            if (item != null)
            {
                container.Inventory.Put(item);
                p.Inventory.Take(thingId);
                return $"You have put {item.Name} in {container.Name}.";
            }
            else
            {
                return $"There is no {thingId} in your inventory.";
            }
        }
    }
}
