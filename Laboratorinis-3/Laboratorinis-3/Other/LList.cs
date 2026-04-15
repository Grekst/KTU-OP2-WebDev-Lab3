using System;
using System.Collections;
using System.Collections.Generic;

namespace Laboratorinis_3
{
    /// <summary>
    /// Generic singly-linked list with sentinel head and tail nodes.
    /// Implements IEnumerable&lt;T&gt; so it can be used in foreach loops
    /// and passed to any method accepting IEnumerable&lt;T&gt;.
    /// </summary>
    public sealed class LList<T> : IEnumerable<T>
    {
        // ── mazgo klasė ───────────────────────────────────────────────
        private sealed class Node
        {
            public T Data { get; set; }
            public Node Link { get; set; }
            public Node(T data, Node link) { Data = data; Link = link; }
        }

        // ── laukai ────────────────────────────────────────────────────
        private readonly Node head;
        private readonly Node tail;
        private Node headFifo;  // paskutinis įterptas elementas
        private Node current;   // iteratoriaus rodyklė

        // ── konstruktorius ────────────────────────────────────────────
        public LList()
        {
            tail = new Node(default(T), null);
            head = new Node(default(T), tail);
            headFifo = head;
            current = null;
        }

        // ── CQS komandos (keičia būseną, nieko negrąžina) ─────────────

        /// <summary>Prideda elementą į sąrašo galą.</summary>
        public void Append(T item)
        {
            headFifo.Link = new Node(item, tail);
            headFifo = headFifo.Link;
        }

        /// <summary>Nustate iteratorių į pradžią.</summary>
        public void Begin() { current = head.Link; }

        /// <summary>Perkelia iteratorių į kitą elementą.</summary>
        public void Next() { current = current.Link; }

        /// <summary>Išsaugo iteratoriaus poziciją (rekursijai).</summary>
        private Node saved;
        public void SavePosition() { saved = current; }
        public void RestorePosition() { current = saved; }

        /// <summary>Pašalina paskutinį elementą (backtracking).</summary>
        public void RemoveLast()
        {
            if (head.Link == tail) return;
            Node temp = head;
            while (temp.Link != headFifo) temp = temp.Link;
            temp.Link = tail;
            headFifo = temp;
        }

        // ── CQS užklausos (neskeičia būsenos, grąžina reikšmę) ────────

        /// <summary>Tikrina ar iteratorius dar turi elementų.</summary>
        public bool Exist() { return current != tail; }

        /// <summary>Grąžina dabartinį elementą.</summary>
        public T Get() { return current.Data; }

        /// <summary>Grąžina pirmą elementą arba default jei tuščias.</summary>
        public T GetFirst()
        {
            return head.Link != tail ? head.Link.Data : default(T);
        }

        /// <summary>Elementų skaičius.</summary>
        public int Count()
        {
            int n = 0;
            for (Begin(); Exist(); Next()) n++;
            return n;
        }

        /// <summary>Randa elementą pagal predikato sąlygą.</summary>
        public T Find(Func<T, bool> predicate)
        {
            for (Begin(); Exist(); Next())
                if (predicate(current.Data))
                    return current.Data;
            return default(T);
        }

        /// <summary>Tikrina ar sąrašas turi elementą tenkinantį predikato sąlygą.</summary>
        public bool Contains(Func<T, bool> predicate)
        {
            return Find(predicate) != null;
        }

        // ── rūšiavimas (bubble sort naudojant IComparable) ─────────────
        /// <summary>
        /// Rūšiuoja sąrašą naudojant nurodytą lygintoją.
        /// Nereikalauja, kad T implementuotų IComparable — galima nurodyti bet kokį Comparison&lt;T&gt;.
        /// </summary>
        public void Sort(Comparison<T> comparison)
        {
            for (Node i = head.Link; i != tail; i = i.Link)
                for (Node j = i.Link; j != tail; j = j.Link)
                    if (comparison(i.Data, j.Data) > 0)
                    {
                        T tmp = i.Data;
                        i.Data = j.Data;
                        j.Data = tmp;
                    }
        }

        // ── IEnumerable<T> ─────────────────────────────────────────────
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
    }
}
