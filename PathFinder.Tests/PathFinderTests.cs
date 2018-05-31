using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PathFinder.Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void PathFinder_NCardList_NormalExecution()
        {
            PathFinder finder = new PathFinder();

            var first = new PathNode {CityFrom = "Melbourne", CityTo = "Keln"};
            var third = new PathNode {CityFrom = "Moscow", CityTo = "Paris"};
            var second = new PathNode {CityFrom = "Keln", CityTo = "Moscow"};

            finder.AddNode(first);
            finder.AddNode(third);
            finder.AddNode(second);

            ICollection<PathNode> result = finder.FindOptimal();

            CollectionAssert.AreEqual(new[]
            {
                first,
                second,
                third
            }, result);
        }

        [Test]
        public void PathFinder_EmptyNodeList_ShouldBeEmptyResult()
        {
            PathFinder finder = new PathFinder();
            ICollection<PathNode> result =  finder.FindOptimal();
            Assert.IsEmpty(result);
        }

        [Test]
        public void PathFinder_OneCardNodeList_ShouldBeReturnOneCard()
        {
            PathFinder finder = new PathFinder(new PathNode { CityFrom = "Moscow", CityTo = "Berlin" });
            ICollection<PathNode> result = finder.FindOptimal();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(result.First().CityFrom, "Moscow");
            Assert.AreEqual(result.First().CityTo, "Berlin");
        }

        [Test]
        public void PathFinder_OneCardCycle_ShouldThrowException()
        {
            PathFinder finder = new PathFinder(new PathNode { CityFrom = "Moscow", CityTo = "Moscow" });
            Assert.Throws<Exception>(() => finder.FindOptimal());
        }

        [Test]
        public void PathFinder_NCardCycle_ShouldThrowException()
        {
            PathFinder finder = new PathFinder(new PathNode { CityFrom = "Moscow", CityTo = "Voronezh" }, new PathNode { CityFrom = "Voronezh", CityTo = "Moscow" });
            Assert.Throws<Exception>(() => finder.FindOptimal());
        }

        [Test]
        public void PathFinder_NCardWithoutLink_ShouldThrowException()
        {
            PathFinder finder = new PathFinder(new PathNode { CityFrom = "Moscow", CityTo = "Voronezh" }, new PathNode { CityFrom = "Paris", CityTo = "Berlin" });
            Assert.Throws<Exception>(() => finder.FindOptimal());
        }
    }
}
