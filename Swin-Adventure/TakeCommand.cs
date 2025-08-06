using System;

namespace Swin_Adventure
{
    public class TakeCommand : Command
    {
        public TakeCommand() : base(new string[] {"pickup", "take"}) { }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 2 & text.Length != 4)
            {
                return "I don't know how to take item like that.";
            }
            else
            {
                if (!AreYou(text[0].ToLower()))
                {
                    return "Error in take input";
                }
                else
                {
                    if (text.Length == 2)
                    {
                        Item item = p.Location.Locate(text[1]) as Item;
                        if (item == null)
                        {
                            return $"I can't find the {text[1]}.";
                        }
                        else
                        {
                            p.Location.Inventory.Take(text[1]);
                            p.Inventory.Put(item);
                            return $"You have taken {item.Name} from {p.Location.Name}.";
                        }
                    }
                    else
                    {
                        if (text[2].ToLower() != "from")
                        {
                            return "Where is the item to pick up from?";
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
                                return TakeFrom(p, text[1], container);
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
        private string TakeFrom(Player p,string thingId, IHaveInventory container)
        {
            Item item = container.Locate(thingId) as Item;
            if (item != null)
            {
                container.Inventory.Take(thingId);
                p.Inventory.Put(item);
                return $"You have taken {item.Name} from {container.Name}.";
            }
            else
            {
                return $"There is no {thingId} in {container.Name}.";
            }
        }
    }
}
