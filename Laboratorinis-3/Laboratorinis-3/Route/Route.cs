using System;
using System.Text;

namespace Laboratorinis_3
{
    /// <summary>
    /// Represents a single travel route: an ordered list of cities and total distance.
    /// </summary>
    public class Route
    {
        public LList<City> Cities { get; private set; }
        public int TotalDistance { get; set; }

        public Route()
        {
            Cities = new LList<City>();
            TotalDistance = 0;
        }

        public void AddCity(City city, int distance = 0)
        {
            Cities.Append(city);
            TotalDistance += distance;
        }

        public Route Clone()
        {
            Route clone = new Route();
            foreach (City c in Cities)
                clone.Cities.Append(c);
            clone.TotalDistance = TotalDistance;
            return clone;
        }

        /// <summary>Grąžina pirmojo miesto pavadinimą (rūšiavimui).</summary>
        public string FirstCityName() => Cities.GetFirst()?.Name ?? string.Empty;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (City c in Cities)
                sb.Append(c.Name).Append(" -> ");
            return sb.ToString().TrimEnd(' ', '-', '>') +
                   string.Format(" | Ilgis: {0} km", TotalDistance);
        }
    }
}
