using System;

namespace Laboratorinis_3
{
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

        /// <summary>
        /// Compares city names by alphabet order
        /// </summary>
        /// <param name="other">The other city to compare to</param>
        /// <returns></returns>
        public int CompareTo(City other)
        {
            if (other == null) return 1;

            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Checks if city names match
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(City other)
        {
            if (other == null) return false;

            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Override for the equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as City);
        }

        public override int GetHashCode()
        {
            return Name == null ? 0 : StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
        }


        /// <summary>
        /// String override for formatting cities in tables
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ({1} gyv.)", Name, Population.ToString("N0"));
        }

    }
}
