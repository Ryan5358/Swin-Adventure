using System;

namespace Swin_Adventure
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] { "move", "go", "leave", "head" }) { }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 2)
            {
                return "I don't know how to move like that.";
            }
            else
            {
                if (!AreYou(text[0].ToLower()))
                {
                    return "Error in move input";
                }
                else
                {
                    Path path = p.Location.FetchPath(text[1]);
                    if (path == null)
                    {
                        return "I can't move there.";
                    }
                    else
                    {
                        path.MovePlayer(p);
                        return path.FullDescription;
                    }
                }
            }
        }
    }
}
