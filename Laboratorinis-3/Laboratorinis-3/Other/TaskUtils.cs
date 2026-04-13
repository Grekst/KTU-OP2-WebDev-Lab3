using System;

namespace Laboratorinis_3
{
    /// <summary>
    /// Responsible only for computations: route finding, backtracking, sorting.
    /// Does not read files, does not read input, does not format output.
    /// </summary>
    public static class TaskUtils
    {
        // ── viešas įėjimo taškas ───────────────────────────────────────

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

            if (startCity == null
                || startCity.Population >= maxPop
                || string.Equals(startCity.Name, unwantedCity, StringComparison.OrdinalIgnoreCase))
                return results;

            Route currentPath = new Route();
            currentPath.AddCity(startCity);

            GenerateRoutes(startCity, currentPath, allCities, allRoads,
                           maxPop, minRouteLen, unwantedCity, results);
            return results;
        }

        public static void SortRoutes(LList<Route> routes)
        {
            routes.Sort((a, b) =>
            {
                int byDist = a.TotalDistance.CompareTo(b.TotalDistance);
                if (byDist != 0) return byDist;
                return string.Compare(a.FirstCityName(), b.FirstCityName(),
                                      StringComparison.OrdinalIgnoreCase);
            });
        }

        // ── privatus DFS ───────────────────────────────────────────────

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
                City nextCity = allCities.Find(c => string.Equals(
                                      c.Name, nextName,
                                      StringComparison.OrdinalIgnoreCase));

                if (nextCity == null) continue;
                if (nextCity.Population >= maxPop) continue;
                if (string.Equals(nextCity.Name, unwantedCity,
                                  StringComparison.OrdinalIgnoreCase)) continue;
                if (IsAlreadyVisited(currentPath.Cities, nextCity.Name)) continue;

                currentPath.AddCity(nextCity, road.Distance);

                allRoads.SavePosition();
                GenerateRoutes(nextCity, currentPath, allCities, allRoads,
                               maxPop, minRouteLen, unwantedCity, results);
                allRoads.RestorePosition();

                currentPath.TotalDistance -= road.Distance;
                currentPath.Cities.RemoveLast();
            }
        }

        private static bool IsAlreadyVisited(LList<City> cities, string cityName)
        {
            foreach (City c in cities)
                if (string.Equals(c.Name, cityName, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
}
