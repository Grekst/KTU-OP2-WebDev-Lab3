using Laboratorinis_3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

namespace Laboratorinis_3
{
    public static class InOut
    {

        /// <summary>
        /// Reads file data from path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFileData(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }


        /// <summary>
        /// Reads city data from textbox
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public static LList<City> ReadCities(TextBox tb)
        {
            LList<City> list = new LList<City>();
            using (StringReader reader = new StringReader(tb.Text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    TryAppendCity(line, list);
                }
            }
            return list;
        }

        /// <summary>
        /// Reads road data from textbox
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public static LList<Road> ReadRoads(TextBox tb)
        {
            LList<Road> list = new LList<Road>();
            using (StringReader reader = new StringReader(tb.Text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    TryAppendRoad(line, list);
                }
            }
            return list;
        }

        /// <summary>
        /// A class used for trimming whitespaces from read text
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public static string ReadText(TextBox tb)
        {
            return tb.Text.Trim();
        }

        /// <summary>
        /// Trims intagers from textboxes
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public static int ReadInt(TextBox tb)
        {
            int val;
            return int.TryParse(tb.Text.Trim(), out val) ? val : 0;
        }

        /// <summary>
        /// Prints the formatted route list
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="output"></param>
        public static void DisplayRoutes(IEnumerable<Route> routes, Literal output)
        {
            output.Text = BuildRoutesTable(routes);
        }

        /// <summary>
        /// Outputs a formatted city list
        /// </summary>
        /// <param name="cities"></param>
        /// <param name="output"></param>
        public static void DisplayCities(IEnumerable<City> cities, Literal output)
        {
            output.Text = BuildCitiesTable(cities);
        }

        /// <summary>
        /// Outputs a formatted road list
        /// </summary>
        /// <param name="roads"></param>
        /// <param name="output"></param>
        public static void DisplayRoads(IEnumerable<Road> roads, Literal output)
        {
            output.Text = BuildRoadsTable(roads);
        }

        /// <summary>
        /// Prints errors into labels
        /// </summary>
        /// <param name="message"></param>
        /// <param name="lbl"></param>
        public static void ShowError(string message, Label lbl)
        {
            lbl.Text = message;
            lbl.CssClass = "msg-error";
        }

        /// <summary>
        /// Prints success into labels
        /// </summary>
        /// <param name="message"></param>
        /// <param name="lbl"></param>
        public static void ShowSuccess(string message, Label lbl)
        {
            lbl.Text = message;
            lbl.CssClass = "msg-success";
        }

        /// <summary>
        /// Clears error labels
        /// </summary>
        /// <param name="lbl"></param>
        public static void ClearMessage(Label lbl)
        {
            lbl.Text = string.Empty;
            lbl.CssClass = string.Empty;
        }


        /// <summary>
        /// Formats and prints initial data into file
        /// </summary>
        /// <param name="roads"></param>
        /// <param name="cities"></param>
        /// <param name="startCity"></param>
        /// <param name="maxPop"></param>
        /// <param name="minDist"></param>
        /// <param name="avoidCity"></param>
        /// <param name="filePath"></param>
        public static void SaveInitialData(
            IEnumerable<Road> roads,
            IEnumerable<City> cities,
            string startCity,
            int maxPop,
            int minDist,
            string avoidCity,
            string filePath)
        {
            string content = FormatInitialData(roads, cities, startCity, maxPop, minDist, avoidCity);
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        /// <summary>
        /// A method used for formatting initial data into a table
        /// </summary>
        /// <param name="roads"></param>
        /// <param name="cities"></param>
        /// <param name="startCity"></param>
        /// <param name="maxPop"></param>
        /// <param name="minDist"></param>
        /// <param name="avoidCity"></param>
        /// <returns></returns>
        private static string FormatInitialData(
            IEnumerable<Road> roads,
            IEnumerable<City> cities,
            string startCity, int maxPop, int minDist, string avoidCity)
        {
            StringBuilder sb = new StringBuilder();
            string line = new string('=', 62);
            string thin = new string('-', 62);

            sb.AppendLine(line);
            sb.AppendLine(Center("PRADINIAI DUOMENYS", 62));
            sb.AppendLine(Center(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 62));
            sb.AppendLine(line);

            sb.AppendLine();
            sb.AppendLine("PARAMETRAI");
            sb.AppendLine(thin);
            sb.AppendLine(Row("Pradinis miestas", startCity));
            sb.AppendLine(Row("Maks. populiacija", maxPop.ToString()));
            sb.AppendLine(Row("Min. atstumas (km)", minDist.ToString()));
            sb.AppendLine(Row("Nelankytinas miestas",
                string.IsNullOrWhiteSpace(avoidCity) ? "(nenurodytas)" : avoidCity));
            sb.AppendLine(thin);

            sb.AppendLine();
            sb.AppendLine("KELIAI (U8a.txt)");
            sb.AppendLine(thin);
            sb.AppendLine(string.Format("  {0,-22} {1,-22} {2}", "Miestas A", "Miestas B", "Km"));
            sb.AppendLine(thin);
            foreach (Road r in roads)
                sb.AppendLine(string.Format("  {0,-22} {1,-22} {2}", r.Start, r.Destination, r.Distance));
            sb.AppendLine(thin);

            sb.AppendLine();
            sb.AppendLine("MIESTAI (U8b.txt)");
            sb.AppendLine(thin);
            sb.AppendLine(string.Format("  {0,-26} {1}", "Miestas", "Gyventojų"));
            sb.AppendLine(thin);
            foreach (City c in cities)
                sb.AppendLine(string.Format("  {0,-26} {1:N0}", c.Name, c.Population));
            sb.AppendLine(thin);

            return sb.ToString();
        }

        /// <summary>
        /// Creates a table of routes
        /// </summary>
        /// <param name="routes"></param>
        /// <returns></returns>
        private static string BuildRoutesTable(IEnumerable<Route> routes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class=\"result-table\">");
            sb.Append("<thead><tr>");
            sb.Append("<th>Nr.</th><th>Maršrutas</th><th>Ilgis (km)</th>");
            sb.Append("</tr></thead><tbody>");

            int nr = 0;
            bool any = false;
            foreach (Route r in routes)
            {
                any = true;
                nr++;
                string rowClass = nr % 2 == 0 ? "row-even" : "row-odd";
                sb.AppendFormat(
                    "<tr class=\"{0}\"><td class=\"td-nr\">{1}</td><td class=\"td-route\">{2}</td><td class=\"td-dist\">{3}</td></tr>",
                    rowClass, nr, RouteToPath(r), r.TotalDistance);
            }

            if (!any)
            {
                sb.Append("<tr><td colspan=\"3\" class=\"td-empty\">Maršrutų nerasta.</td></tr>");
            }


            sb.Append("</tbody></table>");

            if (any)
            {
                sb.AppendFormat("<p class=\"result-count\">Iš viso maršrutų: <strong>{0}</strong></p>", nr);
            }

            return sb.ToString();
        }


        /// <summary>
        /// Creates a table of cities
        /// 
        /// </summary>
        /// <param name="cities"></param>
        /// <returns></returns>
        private static string BuildCitiesTable(IEnumerable<City> cities)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class=\"result-table\">");
            sb.Append("<thead><tr><th>Miestas</th><th>Gyventojų</th></tr></thead><tbody>");
            int nr = 0;
            foreach (City c in cities)
            {
                nr++;
                sb.AppendFormat(
                    "<tr class=\"{0}\"><td>{1}</td><td class=\"td-dist\">{2:N0}</td></tr>",
                    nr % 2 == 0 ? "row-even" : "row-odd",
                    HtmlEncode(c.Name), c.Population);
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }

        /// <summary>
        /// Creates a table of roads
        /// </summary>
        /// <param name="roads"></param>
        /// <returns></returns>
        private static string BuildRoadsTable(IEnumerable<Road> roads)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class=\"result-table\">");
            sb.Append("<thead><tr><th>Miestas A</th><th>Miestas B</th><th>Km</th></tr></thead><tbody>");
            int nr = 0;
            foreach (Road r in roads)
            {
                nr++;
                sb.AppendFormat(
                    "<tr class=\"{0}\"><td>{1}</td><td>{2}</td><td class=\"td-dist\">{3}</td></tr>",
                    nr % 2 == 0 ? "row-even" : "row-odd",
                    HtmlEncode(r.Start), HtmlEncode(r.Destination), r.Distance);
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }

        /// <summary>
        /// Turns a route into a string and formats it for a table
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private static string RouteToPath(Route r)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (City c in r.Cities)
            {
                if (!first) sb.Append(" &rarr; ");
                sb.Append(HtmlEncode(c.Name));
                first = false;
            }
            return sb.ToString();
        }


        /// <summary>
        /// Tries to append a city if criteria match
        /// </summary>
        /// <param name="line"></param>
        /// <param name="list"></param>
        private static void TryAppendCity(string line, LList<City> list)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            string[] p = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            if (p.Length < 2) return;

            int pop;
            if (int.TryParse(p[1].Trim(), out pop))
            {
                list.Append(new City(p[0].Trim(), pop));
            }
        }

        /// <summary>
        /// Tries to append road if criteria match
        /// </summary>
        /// <param name="line"></param>
        /// <param name="list"></param>
        private static void TryAppendRoad(string line, LList<Road> list)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            string[] p = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            if (p.Length < 3) return;

            int dist;
            if (int.TryParse(p[2].Trim(), out dist))
            {
                list.Append(new Road(p[0].Trim(), p[1].Trim(), dist));
            }
        }


        /// <summary>
        /// A method used for formatting table rows
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string Row(string label, string value)
        {
            return string.Format("  {0,-26} {1}", label + ":", value);
        }

        /// <summary>
        /// A method used for formatting the center of the table
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static string Center(string text, int width)
        {
            if (text.Length >= width) return text;
            return new string(' ', (width - text.Length) / 2) + text;
        }

        /// <summary>
        /// A method used for encoding strings for the table
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string HtmlEncode(string s)
        {
            return System.Web.HttpUtility.HtmlEncode(s ?? string.Empty);
        }
    }
}