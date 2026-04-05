using System;
using System.IO;

namespace Laboratorinis_2
{
    public class InOut
    {
        /// <summary>
        /// Reads all data from a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>A string of all data</returns>
        public static string ReadFileData(string path)
        {
            string content;

            using (StreamReader reader = new StreamReader(path))
            {
                content = reader.ReadToEnd();
            }

            return content;
        }

        /// <summary>
        /// Creates a linked list from input string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static LListCity ReadCity(string text)
        {
            LListCity city = new LListCity();
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length >= 2)
                    {
                        string name = parts[0].Trim();
                        int population;
                        if (int.TryParse(parts[1].Trim(), out population))
                        {
                            city.Append(new City(name, population));
                        }
                    }
                }
            }
            return city;
        }

        /// <summary>
        /// Creates a road linked list from input string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static LListRoad ReadRoad(string text)
        {
            LListRoad road = new LListRoad();
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length >= 3)
                    {
                        string start = parts[0].Trim();
                        string destination = parts[1].Trim();
                        int distance;
                        if (int.TryParse(parts[2].Trim(), out distance))
                        {
                            road.Append(new Road(start, destination, distance));
                        }
                    }
                }
            }
            return road;
        }

        /// <summary>
        /// Writes text string into path
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        public static void WriteContentsToFile(string filePath, string text)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            File.WriteAllText(filePath, text);
        }

    }
}