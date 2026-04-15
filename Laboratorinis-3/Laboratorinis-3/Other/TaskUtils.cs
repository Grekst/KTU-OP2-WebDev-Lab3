using System;

namespace Laboratorinis_3
{
    public static class TaskUtils
    {
        /// <summary>
        /// A method used for finding all possible routes that match inputed criteria
        /// </summary>
        /// <param name="allCities"></param>
        /// <param name="allRoads"></param>
        /// <param name="startCityName"></param>
        /// <param name="maxPop"></param>
        /// <param name="minRouteLen"></param>
        /// <param name="unwantedCity"></param>
        /// <returns></returns>
        public static LList<Route> FindAllRoutes(
            LList<City> allCities,
            LList<Road> allRoads,
            string startCityName,
            int maxPop,
            int minRouteLen,
            string unwantedCity)
        {
            LList<Route> results = new LList<Route>();
            City startCity = allCities.Find(c => string.Equals(
                                         c.Name, startCityName,
                                         StringComparison.OrdinalIgnoreCase));

            if (startCity == null || startCity.Population >= maxPop || string.Equals(startCity.Name, unwantedCity, StringComparison.OrdinalIgnoreCase))
            {
                return results;
            }

            Route currentPath = new Route();
            currentPath.AddCity(startCity);

            GenerateRoutes(startCity, currentPath, allCities, allRoads, maxPop, minRouteLen, unwantedCity, results);
            return results;
        }

        public static void SortRoutes(LList<Route> routes)
        {
            routes.Sort((a, b) =>
            {
                int byDist = a.TotalDistance.CompareTo(b.TotalDistance);
                if (byDist != 0) return byDist;
                return string.Compare(a.FirstCityName(), b.FirstCityName(), StringComparison.OrdinalIgnoreCase);
            });
        }

        /// <summary>
        /// Generates possible routes that comply with inputed parameters
        /// </summary>
        /// <param name="currentCity"></param>
        /// <param name="currentPath"></param>
        /// <param name="allCities"></param>
        /// <param name="allRoads"></param>
        /// <param name="maxPop"></param>
        /// <param name="minRouteLen"></param>
        /// <param name="unwantedCity"></param>
        /// <param name="results"></param>
        private static void GenerateRoutes(
            City currentCity,
            Route currentPath,
            LList<City> allCities,
            LList<Road> allRoads,
            int maxPop,
            int minRouteLen,
            string unwantedCity,
            LList<Route> results)
        {
            if (currentPath.TotalDistance > minRouteLen)
                results.Append(currentPath.Clone());

            for (allRoads.Begin(); allRoads.Exist(); allRoads.Next())
            {
                Road road = allRoads.Get();

                if (!road.ConnectsTo(currentCity.Name)) continue;

                string nextName = road.OtherCity(currentCity.Name);
                City nextCity = allCities.Find(c => string.Equals(c.Name, nextName, StringComparison.OrdinalIgnoreCase));

                if (nextCity == null) continue;
                if (nextCity.Population >= maxPop) continue;
                if (string.Equals(nextCity.Name, unwantedCity, StringComparison.OrdinalIgnoreCase)) continue;
                if (IsAlreadyVisited(currentPath.Cities, nextCity.Name)) continue;

                currentPath.AddCity(nextCity, road.Distance);

                allRoads.SavePosition();
                GenerateRoutes(nextCity, currentPath, allCities, allRoads, maxPop, minRouteLen, unwantedCity, results);
                allRoads.RestorePosition();

                currentPath.TotalDistance -= road.Distance;
                currentPath.Cities.RemoveLast();
            }
        }

        /// <summary>
        /// Checks if a city has already been visited when constructing a route
        /// </summary>
        /// <param name="cities"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        private static bool IsAlreadyVisited(LList<City> cities, string cityName)
        {
            foreach (City c in cities)
            {
                if (string.Equals(c.Name, cityName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
