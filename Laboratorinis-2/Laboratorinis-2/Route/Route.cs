using System.Text;

namespace Laboratorinis_2
{
    public class Route
    {
        public LListCity Cities { get; set; }
        public int TotalDistance { get; set; }

        /// <summary>
        /// A blank constructor
        /// </summary>
        public Route()
        {
            Cities = new LListCity();
            TotalDistance = 0;
        }

        /// <summary>
        /// Adds a city to the route
        /// </summary>
        /// <param name="city"></param>
        /// <param name="distance"></param>
        public void AddCity(City city, int distance = 0)
        {
            Cities.Append(city);
            TotalDistance += distance;
        }

        /// <summary>
        /// Creates a copy of the route
        /// </summary>
        /// <returns></returns>
        public Route Clone()
        {
            Route clone = new Route();
            for (Cities.Begin(); Cities.Exist(); Cities.Next())
            {
                clone.Cities.Append(Cities.GetCity());
            }
            clone.TotalDistance = this.TotalDistance;
            return clone;
        }

        /// <summary>
        /// A string override used for formatting
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (Cities.Begin(); Cities.Exist(); Cities.Next())
            {
                sb.Append(Cities.GetCity().Name).Append(" -> ");
            }
            return $"{sb.ToString().TrimEnd(' ', '-', '>')} | Ilgis: {TotalDistance} km";
        }
    }
}