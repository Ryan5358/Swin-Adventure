using System;

namespace Swin_Adventure
{
    public class LookCommand : Command
    {
        public LookCommand () : base(new string[] { "look" }) { }
        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 1 & text.Length != 3 & text.Length != 5)
            {
                return "I don't know how to look like that."; 
            }
            else
            {
                if (!AreYou(text[0].ToLower()))    
                {
                    return "Error in look input";
                }
                else
                {
                    if (text.Length == 1)
                    {
                        return p.Location.FullDescription;
                    }
                    else
                    {
                        if (text[1].ToLower() != "at")
                        {
                            return "What do you want to look at?";
                        }
                        else
                        {
                            if (text.Length == 3)
                            {
                                if (p.Locate(text[2]) == null)
                                {
                                    return $"I can't find the {text[2]}.";
                                }
                                else
                                {
                                    return p.Locate(text[2]).FullDescription;
                                }
                            }
                            else
                            {
                                if (text[3].ToLower() != "in")
                                {
                                    return "What do you want to look in?";
                                }
                                else
                                {
                                    IHaveInventory container = FetchContainer(p, text[4]);
                                    if (container == null)
                                    {
                                        return $"I cannot find the {text[4]}.";
                                    }
                                    else
                                    {
                                        return LookAtIn(text[2], container);
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
        }
        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            if (p.Locate(containerId)!=null)
            {
                return p.Locate(containerId) as IHaveInventory;
            }
            return null;
        }
        private string LookAtIn (string thingId, IHaveInventory container)
        {  
            if(container.Locate(thingId) != null)
            {
                return container.Locate(thingId).FullDescription;
            }
            else
            {
                return $"I cannot find the {thingId} in {container.Name}.";
            }
        }
    }
}
