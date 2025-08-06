using System;

namespace Swin_Adventure
{
    public class Path : GameObject
    {
        private Location _destination;

        public Path(string[] ids, string name, string desc, Location destination) : base(ids, name, desc)
        {
            _destination = destination;
        }

        public void MovePlayer(Player p)
        {
            p.Location = _destination;
        }

        public override string FullDescription
        {
            get
            {
                return $"You head {Name}.\nYou {base.FullDescription}.\nYou have arrived in {_destination.Name}.";
            }
        }
        
        public Location Destination
        {
            get { return _destination; }
        }
    }
}
