namespace Laboratorinis_2
{
    public class LListRoute
    {
        private sealed class Node
        {
            public Route Data { get; set; }
            public Node Link { get; set; }
            public Node(Route data, Node link) { Data = data; Link = link; }
        }

        private Node head, tail, last, current;

        /// <summary>
        /// A blank constructor for linked list
        /// </summary>
        public LListRoute()
        {
            tail = new Node(null, null);
            head = new Node(null, tail);
            last = head;
        }

        /// <summary>
        /// Appends a node inside the linked list
        /// </summary>
        /// <param name="route"></param>
        public void Append(Route route)
        {
            last.Link = new Node(route, tail);
            last = last.Link;
        }

        public void Begin()
        {
            current = head.Link;
        }
        public void Next()
        {
            current = current.Link;
        }
        public bool Exist()
        {
            return current != tail;
        }

        /// <summary>
        /// Gets the route data
        /// </summary>
        /// <returns></returns>
        public Route Get()
        {
            return current.Data;
        }

        /// <summary>
        /// Sorts the routes by total travel distance
        /// </summary>
        public void Sort()
        {
            for (Node i = head.Link; i != tail; i = i.Link)
            {
                for (Node j = i.Link; j != tail; j = j.Link)
                {
                    string nameI = i.Data.Cities.GetFirstCityName();
                    string nameJ = j.Data.Cities.GetFirstCityName();

                    if (i.Data.TotalDistance > j.Data.TotalDistance ||
                       (i.Data.TotalDistance == j.Data.TotalDistance && string.Compare(nameI, nameJ) > 0))
                    {
                        Route temp = i.Data;
                        i.Data = j.Data;
                        j.Data = temp;
                    }
                }
            }
        }
    }
}