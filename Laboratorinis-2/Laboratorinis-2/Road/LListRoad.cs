namespace Laboratorinis_2
{
    public class LListRoad
    {
        /// <summary>
        /// A single linked list node
        /// </summary>
        private sealed class Node
        {
            public Road Data { get; set; }
            public Node Link { get; set; }

            public Node(Road data, Node link)
            {
                Data = data;
                Link = link;
            }
        }

        private Node head;
        private Node tail;
        private Node last;
        private Node current;

        /// <summary>
        /// Constructor
        /// </summary>
        public LListRoad()
        {
            tail = new Node(new Road(), null);
            head = new Node(new Road(), tail);
            last = head;
            current = null;
        }

        /// <summary>
        /// Appends a new element into the linked list
        /// </summary>
        /// <param name="road"></param>
        public void Append(Road road)
        {
            last.Link = new Node(road, tail);
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
        /// Returns data
        /// </summary>
        /// <returns></returns>
        public Road GetRoad()
        {
            return current.Data;
        }

        private Node saved;

        /// <summary>
        /// Saves the current node
        /// </summary>
        public void SavePosition()
        {
            saved = current;
        }

        /// <summary>
        /// Loads the current node from saved
        /// </summary>
        public void RestorePosition()
        {
            current = saved;
        }

        /// <summary>
        /// Returns how many nodes are in the linked list
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            int count = 0;
            for (Begin(); Exist(); Next())
            {
                count++;
            }

            return count;
        }
    }
}