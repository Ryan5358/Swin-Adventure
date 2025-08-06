using NUnit.Framework;
using Swin_Adventure;
using Path = Swin_Adventure.Path;

namespace NUnitTests
{
    public class TestPath
    {
        Path T_path1, T_path2, T_path3;
        Location hallway, small_closet, small_garden;

        [SetUp]
        public void SetUp()
        {
            hallway = new Location("the Hallway", "This is a long well lit hallway");
            T_path1 = new Path(new string[] { "south", "s" }, "South", "go through a door", small_closet);
            T_path2 = new Path(new string[] { "north", "n" }, "North", "go through a door", hallway);
            small_closet = new Location("a small Closet", "This is a small dark closet, with an odd smell");
            T_path3 = new Path(new string[] { "east", "e" }, "East", "travel through a small door, and then crawl a few meters before arriving from the north", small_garden);
            small_garden = new Location("a small Garden", "There are many more shrubs and flowers growing from well tended garden beds");
            
            hallway.SetPaths(new Path[] { T_path1 });
            small_closet.SetPaths(new Path[] { T_path2, T_path3 });
        }

        [TestCase("south", "north", "east")]
        [TestCase("s", "n", "e")]
        public void Test1_PathIsIdentifiable(string toTest1, string toTest2, string toTest3)
        {
            Assert.IsTrue(T_path1.AreYou(toTest1));
            Assert.IsTrue(T_path2.AreYou(toTest2));
            Assert.IsTrue(T_path3.AreYou(toTest3));
        }

        [TestCase("south", "north", "east")]
        [TestCase("s", "n", "e")]
        public void Test2_LocationFindsPath(string toTest1, string toTest2, string toTest3)
        {
            Assert.That(hallway.FetchPath(toTest1), Is.EqualTo(T_path1));
            Assert.That(small_closet.FetchPath(toTest2), Is.EqualTo(T_path2));
            Assert.That(small_closet.FetchPath(toTest3), Is.EqualTo(T_path3));
        }

        [TestCase("west")]
        [TestCase("w")]
        public void Test3_LocationAddsPaths(string toTest)
        {
            Path T_path4 = new Path(new string[] { "west", "w" }, "West", "crawl a few meters south, and then go in through a small door", small_closet);
            small_garden.SetPaths(new Path[] { T_path4 });
            Assert.That(small_garden.FetchPath(toTest), Is.EqualTo(T_path4));
        }

        [TestCase("north")]
        [TestCase("n")]
        [Test]
        public void Test4_LocationRemovesPaths(string toTest)
        {
            small_closet.ClosePaths(new Path[] { T_path2 });
            Assert.That(small_closet.FetchPath(toTest), Is.EqualTo(null));
        }

        [Test]
        public void Test5_PathMovesPlayer()
        {
            Player player = new Player("Fred", "the mighty programmer");
            player.Location = hallway;
            T_path1.MovePlayer(player);
            Assert.That(player.Location, Is.EqualTo(T_path1.Destination));
        }
    }
}
