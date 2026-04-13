using System;

namespace Laboratorinis_3
{
    /// <summary>
    /// Data class representing a city.
    /// Implements IComparable&lt;City&gt; (sort by Name) and IEquatable&lt;City&gt; (equality by Name).
    /// </summary>
    public class City : IComparable<City>, IEquatable<City>
    {
        public string Name { get; set; }
        public int Population { get; set; }

        public City() { Name = null; Population = 0; }

        public City(string name, int population)
        {
            Name = name;
            Population = population;
        }

        // ── IComparable<City> ──────────────────────────────────────────
        /// <summary>Lygina miestus pagal pavadinimą (abėcėlės tvarka).</summary>
        public int CompareTo(City other)
        {
            if (other == null) return 1;
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        // ── IEquatable<City> ───────────────────────────────────────────
        /// <summary>Lygybė pagal pavadinimą (case-insensitive).</summary>
        public bool Equals(City other)
        {
            if (other == null) return false;
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj) => Equals(obj as City);

        public override int GetHashCode() =>
            Name == null ? 0 : StringComparer.OrdinalIgnoreCase.GetHashCode(Name);

        public override string ToString() =>
            string.Format("{0} ({1} gyv.)", Name, Population.ToString("N0"));
    }
}
