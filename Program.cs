using System;
using System.IO;
using System.Collections.Generic;

namespace Production
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("../../../Scabb.clair");

            string line;
            List<string> island = new List<string>();

            while ((line = sr.ReadLine()) != null)
                island.Add(line);

            string cryptedIsland = "";

            string[,] lines = new string[5, 5];

            for (var i = 0; i < island.Count; i++)
            {
                for (var j = 0; j < island[i].Length; j++)
                {
                    double unit = 0;

                    if (i - 1 < 0 || island[i][j] != island[i - 1][j])
                        unit += Math.Pow(2, 0);

                    if (j - 1 < 0 || island[i][j] != island[i][j - 1])
                        unit += Math.Pow(2, 1);

                    if (i + 1 >= island.Count || island[i][j] != island[i + 1][j])
                        unit += Math.Pow(2, 2);

                    if (j + 1 >= island[i].Length || island[i][j] != island[i][j + 1])
                        unit += Math.Pow(2, 3);

                    if (island[i][j] == 'M')
                        unit += 64;

                    if (island[i][j] == 'F')
                        unit += 32;

                    cryptedIsland += unit;

                    if (j < island[j].Length - 1)
                        cryptedIsland += ':';
                }

                if (i < island[i].Length - 1)
                    cryptedIsland += '|';
            }

            Console.WriteLine(cryptedIsland);
        }
    }
}