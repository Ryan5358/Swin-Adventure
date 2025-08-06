using System;
using System.IO;

namespace Swin_Adventure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create player details
            string name, desc;
            Console.WriteLine("Creating a player\n");
            Console.WriteLine("What is your player's name?");
            Console.Write("> ");
            name = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("How would you like to describe your player");
            Console.Write("> ");
            desc = Console.ReadLine();
            Console.Clear();
            
            // Initialising

            // Set up Player
            Player myPlayer = new Player(name, desc);

            // Set up Items
            Item sword = new Item(new string[] { "sword", "bronze sword" }, "a bronze sword", "A short sword cast from bronze");
            Item shovel = new Item(new string[] { "shovel", "spade" }, "a shovel", "A metal shovel");
            Item gem = new Item(new string[] { "gem" }, "a red gem", "A bright red ruby the size of your fist!");
            Item armour = new Item(new string[] { "armour" }, "a set of steel armour", "A full set of armour made from steel");
            Item book = new Item(new string[] { "book" }, "a small book", "A small book with red cover");
            Item pc = new Item(new String[] { "pc", " computer" }, "a small computer", "The light from the monitor of this computer illuminates the room");
            Bag bag = new Bag(new string[] { "bag" }, "the leather bag", "A small brown leather bag");
            Bag backpack = new Bag(new string[] { "backpack" }, "a backpack", "A brown leather medium sized backpack");
            bag.Inventory.Put(gem);

            // Set up Locations
            Location Hallway = new Location("the Hallway", "This is a well lit hallway");
            Location Small_Closet = new Location("a small Closet", "This is a small dark closet, with an odd smell");
            Location Small_Garden = new Location("a small Garden", "There are many more shrubs and flowers growing from well tended garden beds.");
            Location Library = new Location("the Library", "This is an ancient old library");

            // Set up Items in Locations
            // Hallway
            Hallway.Inventory.Put(shovel);
            Hallway.Inventory.Put(sword);

            // Small Closet
            Small_Closet.Inventory.Put(pc);

            // Small Garden
            Small_Garden.Inventory.Put(bag);

            // Library
            Library.Inventory.Put(book);
            Library.Inventory.Put(backpack);

            // Set up Paths for Location
            // Hallway
            Hallway.SetPaths(new Path[] 
                {
                    new Path(new string[] { "south", "s" }, "South", "go through a door", Small_Closet)
                });

            // Small Closet
            Small_Closet.SetPaths(new Path[] 
                {
                    new Path(new string[] { "north", "n" }, "North", "go through a door", Hallway),
                    new Path(new string[] { "east", "e" }, "East", "travel through a small door, and then crawl a few meters before arriving from the north", Small_Garden)
                });

            // Small Garden
            Small_Garden.SetPaths(new Path[] 
                {
                    new Path(new string[] { "south", "s" }, "South", "crawl a few meters south, and then go in through a small door", Small_Closet),
                    new Path(new string[] { "north", "n" }, "North", "walk on the stone path and walk through the garden gate", Library)
                });

            // Library
            Library.SetPaths(new Path[] 
                {
                    new Path(new string[] { "south", "s"}, "South", "head to the library entrance, then continue walking before arriving the garden gate", Small_Garden)
                });

            // Prepare for the game    
            myPlayer.Location = Hallway;
            CommandProcessor cp = new CommandProcessor(
                new Command[] {
                    new LookCommand(),
                    new MoveCommand(),
                    new TakeCommand(),
                    new PutCommand()
                });
            string command = "";
            bool isPlaying = true;

            // Process command inputs
            Console.WriteLine($"Welcome to Swin Adventure!\nYou have arrived in {myPlayer.Location.Name}.");
            while(isPlaying)
            {
                Console.Write("Command -> ");
                command = Console.ReadLine();
                Console.WriteLine();
                if (command != "quit")
                {
                    Console.WriteLine(cp.Execute(myPlayer, command));
                }
                else
                {
                    isPlaying = false;
                    Console.WriteLine("Bye.");
                }
            }
        }
    }
}