using System;
using System.Collections;
using System.Collections.Generic;

namespace Laboratorinis_3
{
    public sealed class LList<T> : IEnumerable<T>
    {
        private sealed class Node
        {
            public T Data { get; set; }
            public Node Link { get; set; }
            public Node(T data, Node link) { Data = data; Link = link; }
        }

        private readonly Node head;
        private readonly Node tail;
        private Node headFifo;  // Last inserted element
        private Node current;   // Pointer

        /// <summary>
        /// Constructor
        /// </summary>
        public LList()
        {
            tail = new Node(default(T), null);
            head = new Node(default(T), tail);
            headFifo = head;
            current = null;
        }

        /// <summary>
        /// Appends an element into the back of the list
        /// </summary>
        /// <param name="item"></param>
        public void Append(T item)
        {
            headFifo.Link = new Node(item, tail);
            headFifo = headFifo.Link;
        }

        /// <summary>
        /// Sets the pointer to the start
        /// </summary>
        public void Begin() { current = head.Link; }

        /// <summary>
        /// Moves the pointer to the next element
        /// </summary>
        public void Next() { current = current.Link; }

        private Node saved;
        public void SavePosition() { saved = current; }
        public void RestorePosition() { current = saved; }

        /// <summary>
        /// Removes the last element
        /// </summary>
        public void RemoveLast()
        {
            if (head.Link == tail) return;
            Node temp = head;
            while (temp.Link != headFifo) temp = temp.Link;
            temp.Link = tail;
            headFifo = temp;
        }

        /// <summary>
        /// Check if pointer has more elements
        /// </summary>
        /// <returns></returns>
        public bool Exist() { return current != tail; }

        /// <summary>
        /// Returns current element
        /// </summary>
        /// <returns></returns>
        public T Get() { return current.Data; }

        /// <summary>
        /// Returns the first element or blank if its missing
        /// </summary>
        /// <returns></returns>
        public T GetFirst()
        {
            return head.Link != tail ? head.Link.Data : default(T);
        }

        /// <summary>
        /// Returns the element count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            int n = 0;
            for (Begin(); Exist(); Next()) n++;
            return n;
        }

        /// <summary>
        /// Finds an element that matches condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T Find(Func<T, bool> condition)
        {
            for (Begin(); Exist(); Next())
            {
                if (condition(current.Data))
                {
                    return current.Data;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Checks if the list contains elements that matches condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool Contains(Func<T, bool> condition)
        {
            return Find(condition) != null;
        }

        /// <summary>
        /// Bubble sorts the list
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<T> comparison)
        {
            for (Node i = head.Link; i != tail; i = i.Link)
            {
                for (Node j = i.Link; j != tail; j = j.Link)
                {
                    if (comparison(i.Data, j.Data) > 0)
                    {
                        T tmp = i.Data;
                        i.Data = j.Data;
                        j.Data = tmp;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node node = head.Link;
            while (node != tail)
            {
                yield return node.Data;
                node = node.Link;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Main Main
        {
            get => default;
            set
            {
            }
        }
    }
}
