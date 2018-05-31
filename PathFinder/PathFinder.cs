using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{

    /// <summary>
    /// O(N) method
    /// </summary>
    public class PathFinder
    {
        private readonly List<PathNode> _pathNodes;

        public PathFinder(params PathNode[] pathNodes)
        {
            _pathNodes = new List<PathNode>(pathNodes);
        }

        public void AddNode(PathNode node)
        {
            _pathNodes.Add(node);
        }

        public ICollection<PathNode> FindOptimal()
        {
            switch (_pathNodes.Count)
            {
                case 1 when _pathNodes[0].CityTo == _pathNodes[0].CityFrom:
                    throw new Exception("Cycle detected");
                case 0:
                case 1:
                    return _pathNodes;
            }

            var citiesTo = _pathNodes.Select(t => t.CityTo);

            var citiesFromt = _pathNodes.ToDictionary(t => t.CityFrom, t => t);

            PathNode firstCard;
            try
            {
                firstCard = _pathNodes.Single(x => !citiesTo.Contains(x.CityFrom));
            }

            catch (InvalidOperationException)
            {
                throw new Exception("Cycle detected");
            }

            var citiesFrom = _pathNodes.ToDictionary(t => t.CityFrom, t => t);

            List<PathNode> result = new List<PathNode>() { firstCard };
            string next = firstCard.CityTo;

            while (true)
            {
                if (!citiesFrom.TryGetValue(next, out PathNode nextCard))
                {
                    return result;
                }

                result.Add(nextCard);
                next = nextCard.CityTo;
            }
        }
    }
}