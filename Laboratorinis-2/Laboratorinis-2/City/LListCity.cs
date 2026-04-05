using System;

namespace Laboratorinis_2
{
    public class LListCity
    {
        /// <summary>
        /// A single linked list node
        /// </summary>
        private sealed class Node
        {
            public City Data { get; set; }
            public Node Link { get; set; }

            public Node(City data, Node link)
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
        public LListCity()
        {
            tail = new Node(null, null);
            head = new Node(null, tail);
            last = head;
            current = null;
        }

        /// <summary>
        /// Appends a new element into the linked list
        /// </summary>
        /// <param name="city"></param>
        public void Append(City city)
        {
            last.Link = new Node(city, tail);
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
        /// Used for getting the node data of a city
        /// </summary>
        /// <returns></returns>
        public City GetCity()
        {
            return current.Data;
        }

        /// <summary>
        /// A method used for finding specific data based on the cities name
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public City Find(string cityName)
        {
            if (string.IsNullOrEmpty(cityName)) return null;
            for (Begin(); Exist(); Next())
            {
                if (current.Data.Name.Trim().Equals(cityName.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return current.Data;
                }
            }
            return null;
        }

        /// <summary>
        /// Used for counting the number of nodes in the linked list
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

        /// <summary>
        /// Returns the name of the first city
        /// </summary>
        /// <returns></returns>
        public string GetFirstCityName()
        {
            if (head.Link != tail)
            {
                return head.Link.Data.Name;
            }
            return string.Empty;
        }

        /// <summary>
        /// Checks if the linked list contains a specific city
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsName(string name)
        {
            for (Begin(); Exist(); Next())
            {
                if (current.Data.Name == name) return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the tail node of the linked list
        /// </summary>
        public void RemoveLast()
        {
            if (head.Link == tail) return;
            Node temp = head;
            while (temp.Link != last) temp = temp.Link;
            temp.Link = tail;
            last = temp;
        }
    }
}