using Laboratorinis_3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorinis_3.Tests
{
    [TestClass]
    public class LListTests
    {
        [TestMethod]
        public void Count_EmptyList_ReturnsZero()
        {
            LList<City> list = new LList<City>();
            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void Count_AfterThreeAppends_ReturnsThree()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Vilnius", 580000));
            list.Append(new City("Kaunas", 290000));
            list.Append(new City("Klaipėda", 145000));
            Assert.AreEqual(3, list.Count());
        }

        [TestMethod]
        public void Append_PreservesOrder()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("A", 1));
            list.Append(new City("B", 2));
            list.Append(new City("C", 3));

            List<string> names = list.Select(c => c.Name).ToList();
            CollectionAssert.AreEqual(new[] { "A", "B", "C" }, names);
        }

        // Begin / Next / Exist / Get

        [TestMethod]
        public void Exist_EmptyList_ReturnsFalse()
        {
            LList<City> list = new LList<City>();
            list.Begin();
            Assert.IsFalse(list.Exist());
        }

        [TestMethod]
        public void BeginExistNextGet_IteratesAllElements()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("X", 10));
            list.Append(new City("Y", 20));

            list.Begin();
            Assert.IsTrue(list.Exist());
            Assert.AreEqual("X", list.Get().Name);

            list.Next();
            Assert.IsTrue(list.Exist());
            Assert.AreEqual("Y", list.Get().Name);

            list.Next();
            Assert.IsFalse(list.Exist());
        }

        // GetFirst

        [TestMethod]
        public void GetFirst_EmptyList_ReturnsNull()
        {
            LList<City> list = new LList<City>();
            Assert.IsNull(list.GetFirst());
        }

        [TestMethod]
        public void GetFirst_NonEmptyList_ReturnsFirstElement()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Vilnius", 580000));
            list.Append(new City("Kaunas", 290000));
            Assert.AreEqual("Vilnius", list.GetFirst().Name);
        }

        // Find

        [TestMethod]
        public void Find_ExistingElement_ReturnsIt()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Vilnius", 580000));
            list.Append(new City("Kaunas", 290000));

            City found = list.Find(c => c.Name == "Kaunas");
            Assert.IsNotNull(found);
            Assert.AreEqual(290000, found.Population);
        }

        [TestMethod]
        public void Find_NonExistingElement_ReturnsNull()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Vilnius", 580000));

            City found = list.Find(c => c.Name == "Nėra");
            Assert.IsNull(found);
        }

        // Contains

        [TestMethod]
        public void Contains_ExistingElement_ReturnsTrue()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Šiauliai", 95000));

            Assert.IsTrue(list.Contains(c => c.Name == "Šiauliai"));
        }

        [TestMethod]
        public void Contains_NonExistingElement_ReturnsFalse()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Šiauliai", 95000));

            Assert.IsFalse(list.Contains(c => c.Name == "Klaipėda"));
        }

        // RemoveLast

        [TestMethod]
        public void RemoveLast_SingleElement_ListBecomesEmpty()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("A", 1));
            list.RemoveLast();
            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void RemoveLast_MultipleElements_RemovesOnlyLast()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("A", 1));
            list.Append(new City("B", 2));
            list.Append(new City("C", 3));
            list.RemoveLast();

            List<string> names = list.Select(c => c.Name).ToList();
            CollectionAssert.AreEqual(new[] { "A", "B" }, names);
        }

        [TestMethod]
        public void RemoveLast_EmptyList_DoesNotThrow()
        {
            LList<City> list = new LList<City>();
            list.RemoveLast();
            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void RemoveLast_CanAppendAgainAfterRemove()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("A", 1));
            list.Append(new City("B", 2));
            list.RemoveLast();
            list.Append(new City("C", 3));

            List<string> names = list.Select(c => c.Name).ToList();
            CollectionAssert.AreEqual(new[] { "A", "C" }, names);
        }

        // SavePosition / RestorePosition

        [TestMethod]
        public void SaveAndRestorePosition_ResumesFromSavedPoint()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("A", 1));
            list.Append(new City("B", 2));
            list.Append(new City("C", 3));

            list.Begin();
            list.Next();
            list.SavePosition();

            list.Next();
            Assert.AreEqual("C", list.Get().Name);

            list.RestorePosition();
            Assert.AreEqual("B", list.Get().Name);
        }

        // Sort

        [TestMethod]
        public void Sort_ByPopulationAscending_CorrectOrder()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Vilnius", 580000));
            list.Append(new City("Alytus", 50000));
            list.Append(new City("Kaunas", 290000));

            list.Sort((a, b) => a.Population.CompareTo(b.Population));

            List<string> names = list.Select(c => c.Name).ToList();
            CollectionAssert.AreEqual(new[] { "Alytus", "Kaunas", "Vilnius" }, names);
        }

        [TestMethod]
        public void Sort_ByNameAlphabetically_CorrectOrder()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Vilnius", 1));
            list.Append(new City("Alytus", 1));
            list.Append(new City("Kaunas", 1));

            list.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));

            List<string> names = list.Select(c => c.Name).ToList();
            CollectionAssert.AreEqual(new[] { "Alytus", "Kaunas", "Vilnius" }, names);
        }

        [TestMethod]
        public void Sort_SingleElement_DoesNotThrow()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("A", 1));
            list.Sort((a, b) => a.CompareTo(b));
            Assert.AreEqual(1, list.Count());
        }

        // IEnumerable<T>

        [TestMethod]
        public void IEnumerable_ForeachIteratesAllElements()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("A", 1));
            list.Append(new City("B", 2));
            list.Append(new City("C", 3));

            List<string> result = new List<string>();
            foreach (City c in list)
                result.Add(c.Name);

            CollectionAssert.AreEqual(new[] { "A", "B", "C" }, result);
        }

        [TestMethod]
        public void IEnumerable_LinqCountWorks()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("A", 100));
            list.Append(new City("B", 200));

            int count = list.Count(c => c.Population > 50);
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void IEnumerable_CanPassToMethodAcceptingIEnumerable()
        {
            LList<City> list = new LList<City>();
            list.Append(new City("Vilnius", 580000));

            IEnumerable<City> asInterface = list;
            Assert.IsNotNull(asInterface.FirstOrDefault());
        }

        [TestMethod]
        public void IEnumerable_EmptyList_ForeachDoesNotIterate()
        {
            LList<City> list = new LList<City>();
            int count = 0;
            foreach (City _ in list) count++;
            Assert.AreEqual(0, count);
        }

        // Road

        [TestMethod]
        public void LListRoad_AppendAndCount_Works()
        {
            LList<Road> list = new LList<Road>();
            list.Append(new Road("Vilnius", "Kaunas", 102));
            list.Append(new Road("Kaunas", "Klaipėda", 212));
            Assert.AreEqual(2, list.Count());
        }

        [TestMethod]
        public void LListRoad_Sort_ByDistanceAscending()
        {
            LList<Road> list = new LList<Road>();
            list.Append(new Road("A", "C", 300));
            list.Append(new Road("A", "B", 100));
            list.Append(new Road("B", "C", 200));

            list.Sort((a, b) => a.CompareTo(b));

            List<int> distances = list.Select(r => r.Distance).ToList();
            CollectionAssert.AreEqual(new[] { 100, 200, 300 }, distances);
        }
    }
}