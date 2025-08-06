using NUnit.Framework;
using Swin_Adventure;
using Path = Swin_Adventure.Path;

namespace NUnitTests
{
    public class TestMoveCommand
    {
        Command T_move;
        Player player;
        Location hallway, small_closet;
        Path path1, path2;

        [SetUp]
        public void Setup()
        {
            player = new Player("Fred", "the mighty programmer");
            hallway = new Location("the Hallway", "a long well lit hallway");
            small_closet = new Location("a small Closet", "a small dark closet, with an odd smell");
            path1 = new Path(new string[] { "south", "s" }, "South", "go through a door", small_closet);
            path2 = new Path(new string[] { "north", "n" }, "North", "go through a door", hallway);

            hallway.SetPaths(new Path[] { path1 });
            small_closet.SetPaths(new Path[] { path2 });

            player.Location = hallway;
            T_move = new MoveCommand();
        }

        [Test, Combinatorial]
        public void Test1_Player_LeavesLocation_OnValidPath(
            [Values("move", "go", "leave", "head")] string action,
            [Values("south", "s")] string direction)
        {
            string[] command = { action, direction };
            Assert.That(T_move.Execute(player, command), Is.EqualTo(path1.FullDescription));
            Assert.That(player.Location, Is.EqualTo(small_closet));
        }

        [Test, Combinatorial]
        public void Test2_Player_RemainsInLocation_OnInvalidPath(
            [Values("move", "go", "leave", "head")] string action,
            [Values("east", "e")] string direction)
        {
            string[] command = { action, direction };
            Assert.That(T_move.Execute(player, command), Is.EqualTo("I can't move there."));
            Assert.That(player.Location, Is.EqualTo(hallway));
        }

        [TestCase("go to the east", ExpectedResult = "I don't know how to move like that.")]
        [TestCase("travel east", ExpectedResult = "Error in move input")]
        public string Test3_InvalidMove(string toTest)
        {
            string[] command = toTest.Split();
            return T_move.Execute(player, command);
        }
    }
}
