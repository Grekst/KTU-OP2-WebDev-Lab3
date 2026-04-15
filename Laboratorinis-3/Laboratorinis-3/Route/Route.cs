using System;
using System.Text;

namespace Laboratorinis_3
{
    public class Route
    {
        public LList<City> Cities { get; private set; }
        public int TotalDistance { get; set; }

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
        public Route()
        {
            Cities = new LList<City>();
            TotalDistance = 0;
        }

        /// <summary>
        /// Adds a city to the route
        /// </summary>
        /// <param name="city">City to add</param>
        /// <param name="distance">Distance to be added to the routes total</param>
        public void AddCity(City city, int distance = 0)
        {
            Cities.Append(city);
            TotalDistance += distance;
        }

        /// <summary>
        /// Creates a deep copy of the route
        /// </summary>
        /// <returns></returns>
        public Route Clone()
        {
            Route clone = new Route();

            foreach (City c in Cities)
            {
                clone.Cities.Append(c);
            }

            clone.TotalDistance = TotalDistance;

            return clone;
        }

        /// <summary>
        /// Returns the name of the first city in route
        /// </summary>
        /// <returns></returns>
        public string FirstCityName()
        {
            return Cities.GetFirst()?.Name ?? string.Empty;
        }

        /// <summary>
        /// Formats the route for the result table
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (City c in Cities)
            {
                sb.Append(c.Name).Append(" -> ");
            }

            return sb.ToString().TrimEnd(' ', '-', '>') + string.Format(" | Ilgis: {0} km", TotalDistance);
        }
    }
}
