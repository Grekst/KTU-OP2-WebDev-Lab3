namespace Laboratorinis_2
{
    public class Road
    {
        public string Start { get; private set; }
        public string Destination { get; private set; }
        public int Distance { get; set; }

        /// <summary>
        /// A blank constructor
        /// </summary>
        public Road()
        {

        }

        /// <summary>
        /// A constructor for the Road class
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
        /// Checks if the start or destination name matches inputed city
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public bool ConnectsTo(string cityName)
        {
            return Start == cityName || Destination == cityName;
        }

        /// <summary>
        /// Checks the oposite end of inputed city name
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public string OtherCity(string cityName)
        {
            if (Start == cityName) return Destination;
            if (Destination == cityName) return Start;

            return null;
        }

        /// <summary>
        /// A string override used for formatting
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0, -20} | {1, -20} | {2, 20} km", Start, Destination, Distance);
        }
    }
}