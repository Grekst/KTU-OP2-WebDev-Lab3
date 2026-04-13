using System;

namespace Laboratorinis_3
{
    /// <summary>
    /// Data class representing a road between two cities.
    /// Implements IComparable&lt;Road&gt; (sort by Distance) and IEquatable&lt;Road&gt; (equality by both city names).
    /// </summary>
    public class Road : IComparable<Road>, IEquatable<Road>
    {
        public string Start { get; private set; }
        public string Destination { get; private set; }
        public int Distance { get; set; }

        public Road() { }

        public Road(string start, string destination, int distance)
        {
            Start = start;
            Destination = destination;
            Distance = distance;
        }

        // ── IComparable<Road> ──────────────────────────────────────────
        /// <summary>Lygina kelius pagal atstumą.</summary>
        public int CompareTo(Road other)
        {
            if (other == null) return 1;
            return Distance.CompareTo(other.Distance);
        }

        // ── IEquatable<Road> ───────────────────────────────────────────
        /// <summary>Lygybė: abu miestai sutampa (neatsižvelgiant į kryptį).</summary>
        public bool Equals(Road other)
        {
            if (other == null) return false;
            bool direct = string.Equals(Start, other.Start, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(Destination, other.Destination, StringComparison.OrdinalIgnoreCase);
            bool reverse = string.Equals(Start, other.Destination, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(Destination, other.Start, StringComparison.OrdinalIgnoreCase);
            return direct || reverse;
        }

        public override bool Equals(object obj) => Equals(obj as Road);

        public override int GetHashCode()
        {
            string a = Start ?? "";
            string b = Destination ?? "";
            // Kryptis nesvarbi — naudojame XOR
            return StringComparer.OrdinalIgnoreCase.GetHashCode(a)
                 ^ StringComparer.OrdinalIgnoreCase.GetHashCode(b);
        }

        // ── pagalbiniai metodai ────────────────────────────────────────
        public bool ConnectsTo(string cityName) =>
            string.Equals(Start, cityName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(Destination, cityName, StringComparison.OrdinalIgnoreCase);

        public string OtherCity(string cityName)
        {
            if (string.Equals(Start, cityName, StringComparison.OrdinalIgnoreCase))
                return Destination;
            if (string.Equals(Destination, cityName, StringComparison.OrdinalIgnoreCase))
                return Start;
            return null;
        }

        public override string ToString() =>
            string.Format("{0} - {1}: {2} km", Start, Destination, Distance);
    }
}
