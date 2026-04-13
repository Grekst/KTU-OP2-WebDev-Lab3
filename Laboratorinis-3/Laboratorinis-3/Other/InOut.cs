using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

namespace Laboratorinis_3
{
    /// <summary>
    /// The only class responsible for all input and output.
    /// Display methods accept IEnumerable&lt;T&gt; — works with LList&lt;T&gt; or any collection.
    /// </summary>
    public static class InOut
    {
        // ══════════════════════════════════════════════════════════════
        // FAILO SKAITYMAS
        // ══════════════════════════════════════════════════════════════

        /// <summary>Skaito failo turinį pagal kelią.</summary>
        public static string ReadFileData(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                return sr.ReadToEnd();
        }

        // ══════════════════════════════════════════════════════════════
        // DUOMENŲ NUSKAITYMAS IŠ TextBox
        // ══════════════════════════════════════════════════════════════

        /// <summary>Nuskaito ir išanalizuoja miestų duomenis iš TextBox teksto.</summary>
        public static LList<City> ReadCities(TextBox tb)
        {
            LList<City> list = new LList<City>();
            using (StringReader reader = new StringReader(tb.Text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    TryAppendCity(line, list);
            }
            return list;
        }

        /// <summary>Nuskaito ir išanalizuoja kelių duomenis iš TextBox teksto.</summary>
        public static LList<Road> ReadRoads(TextBox tb)
        {
            LList<Road> list = new LList<Road>();
            using (StringReader reader = new StringReader(tb.Text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    TryAppendRoad(line, list);
            }
            return list;
        }

        public static string ReadText(TextBox tb) => tb.Text.Trim();
        public static int ReadInt(TextBox tb)
        {
            int val;
            return int.TryParse(tb.Text.Trim(), out val) ? val : 0;
        }

        // ══════════════════════════════════════════════════════════════
        // REZULTATŲ RAŠYMAS Į UI — PRIIMA IEnumerable<T>
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// Išveda maršrutų sąrašą į Literal kontrolį kaip HTML lentelę su CSS stiliais.
        /// Priima IEnumerable&lt;Route&gt; — veikia su LList&lt;Route&gt; ar bet kokia kolekcija.
        /// </summary>
        public static void DisplayRoutes(IEnumerable<Route> routes, Literal output)
        {
            output.Text = BuildRoutesTable(routes);
        }

        /// <summary>
        /// Išveda miestų sąrašą į Literal kontrolį kaip HTML lentelę.
        /// </summary>
        public static void DisplayCities(IEnumerable<City> cities, Literal output)
        {
            output.Text = BuildCitiesTable(cities);
        }

        /// <summary>
        /// Išveda kelių sąrašą į Literal kontrolį kaip HTML lentelę.
        /// </summary>
        public static void DisplayRoads(IEnumerable<Road> roads, Literal output)
        {
            output.Text = BuildRoadsTable(roads);
        }

        /// <summary>Rašo klaidos pranešimą į Label.</summary>
        public static void ShowError(string message, Label lbl)
        {
            lbl.Text = message;
            lbl.CssClass = "msg-error";
        }

        /// <summary>Išvalo pranešimo Label.</summary>
        public static void ClearMessage(Label lbl)
        {
            lbl.Text = string.Empty;
            lbl.CssClass = string.Empty;
        }

        // ══════════════════════════════════════════════════════════════
        // PRADINIŲ DUOMENŲ IŠSAUGOJIMAS Į FAILĄ
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// Suformatuoja ir išsaugo pradinius duomenis į Pradiniai.txt.
        /// Atsakomybė: tik formatavimas + rašymas į diską.
        /// </summary>
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

        // ══════════════════════════════════════════════════════════════
        // PRIVAČIOS HTML LENTELIŲ KŪRIMO FUNKCIJOS
        // ══════════════════════════════════════════════════════════════

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
                    rowClass, nr, HtmlEncode(RouteToPath(r)), r.TotalDistance);
            }

            if (!any)
                sb.Append("<tr><td colspan=\"3\" class=\"td-empty\">Maršrutų nerasta.</td></tr>");

            sb.Append("</tbody></table>");
            if (any)
                sb.AppendFormat("<p class=\"result-count\">Iš viso maršrutų: <strong>{0}</strong></p>", nr);
            return sb.ToString();
        }

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

        private static string RouteToPath(Route r)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (City c in r.Cities)
            {
                if (!first) sb.Append(" &rarr; ");
                sb.Append(c.Name);
                first = false;
            }
            return sb.ToString();
        }

        // ══════════════════════════════════════════════════════════════
        // PRIVAČIOS ANALIZAVIMO FUNKCIJOS
        // ══════════════════════════════════════════════════════════════

        private static void TryAppendCity(string line, LList<City> list)
        {
            if (string.IsNullOrWhiteSpace(line)) return;
            string[] p = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 2) return;
            int pop;
            if (int.TryParse(p[1].Trim(), out pop))
                list.Append(new City(p[0].Trim(), pop));
        }

        private static void TryAppendRoad(string line, LList<Road> list)
        {
            if (string.IsNullOrWhiteSpace(line)) return;
            string[] p = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) return;
            int dist;
            if (int.TryParse(p[2].Trim(), out dist))
                list.Append(new Road(p[0].Trim(), p[1].Trim(), dist));
        }

        // ══════════════════════════════════════════════════════════════
        // MAŽI PAGALBINIAI METODAI
        // ══════════════════════════════════════════════════════════════

        private static string Row(string label, string value) =>
            string.Format("  {0,-26} {1}", label + ":", value);

        private static string Center(string text, int width)
        {
            if (text.Length >= width) return text;
            return new string(' ', (width - text.Length) / 2) + text;
        }

        private static string HtmlEncode(string s) =>
            System.Web.HttpUtility.HtmlEncode(s ?? string.Empty);
    }
}
