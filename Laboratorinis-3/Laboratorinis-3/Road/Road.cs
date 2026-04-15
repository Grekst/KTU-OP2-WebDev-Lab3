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

        public LList<object> LList
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Blank constructor
        /// </summary>
        public Road() { }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        /// <param name="distance"></param>
        public Road(string start, string destination, int distance)
        {
            Start = start;
            Destination = destination;
            Distance = distance;
        }

        /// <summary>
        /// Compares roads by distance
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Road other)
        {
            if (other == null) return 1;
            return Distance.CompareTo(other.Distance);
        }

        /// <summary>
        /// Checks if cities match
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Road other)
        {
            if (other == null) return false;
            bool direct = string.Equals(Start, other.Start, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(Destination, other.Destination, StringComparison.OrdinalIgnoreCase);
            bool reverse = string.Equals(Start, other.Destination, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(Destination, other.Start, StringComparison.OrdinalIgnoreCase);
            return direct || reverse;
        }

        /// <summary>
        /// Override for the Equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Road);
        }

        public override int GetHashCode()
        {
            string a = Start ?? "";
            string b = Destination ?? "";

            return StringComparer.OrdinalIgnoreCase.GetHashCode(a) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(b);
        }

        /// <summary>
        /// Checks if road connects to a specific city
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public bool ConnectsTo(string cityName)
        {
            return string.Equals(Start, cityName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(Destination, cityName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Outputs where road from a city leads to
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public string OtherCity(string cityName)
        {
            if (string.Equals(Start, cityName, StringComparison.OrdinalIgnoreCase))
            {
                return Destination;
            }
            if (string.Equals(Destination, cityName, StringComparison.OrdinalIgnoreCase))
            {
                return Start;
            }
            return null;
        }

        /// <summary>
        /// A string override used for formatting roads
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}: {2} km", Start, Destination, Distance);
        }
    }
}
