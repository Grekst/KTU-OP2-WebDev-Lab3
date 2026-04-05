namespace Laboratorinis_2
{
    public class City
    {
        public string Name { get; set; }
        public int Population { get; set; }

        /// <summary>
        /// Constructor for the City class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="population"></param>
        public City(string name, int population)
        {
            Name = name;
            Population = population;
        }

        /// <summary>
        /// A blank constructor
        /// </summary>
        public City()
        {
            Name = null;
            Population = 0;
        }

        /// <summary>
        /// A string override used for formatting
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0, -20} | {1, 20}", Name, Population);
        }

        /// <summary>
        /// A comparison override
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is City city && Name == city.Name;
        }
    }
}