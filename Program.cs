using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Production
{
    class Program
    {
        static void Main(string[] args)
        {
            // ENCODAGE
            StreamReader sr;

            try
            {
                sr = new StreamReader("../../../Scabb.clair");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            string line;
            List<string> island = new List<string>();

            while ((line = sr.ReadLine()) != null)
                island.Add(line);

            string cryptedIsland = "";

            for (var i = 0; i < island.Count; i++)
            {
                for (var j = 0; j < island[i].Length; j++)
                {
                    double unit = 0;

                    if (i - 1 < 0 || island[i][j] != island[i - 1][j]) // NORD
                        unit += Math.Pow(2, 0);

                    if (j - 1 < 0 || island[i][j] != island[i][j - 1]) // WEST
                        unit += Math.Pow(2, 1);

                    if (i + 1 >= island.Count || island[i][j] != island[i + 1][j]) // SUD
                        unit += Math.Pow(2, 2);

                    if (j + 1 >= island[i].Length || island[i][j] != island[i][j + 1]) // EAST
                        unit += Math.Pow(2, 3);

                    if (island[i][j] == 'M') // MER
                        unit += 64;

                    if (island[i][j] == 'F') // FOREST
                        unit += 32;

                    cryptedIsland += unit;

                    if (j < island[j].Length - 1)
                        cryptedIsland += ':';
                }

                if (i < island[i].Length - 1)
                    cryptedIsland += '|';
            }

            Console.WriteLine(cryptedIsland);

            // DECODAGE

            string[] lines = cryptedIsland.Split('|');

            List<List<int>> units = new List<List<int>>();
            List<List<List<int>>> zones = new List<List<List<int>>>();

            foreach (string lineString in lines)
            {
                units.Add(new List<int> { });

                List<int> splitedLineIntegers = lineString.Split(':').Select(Int32.Parse).ToList();

                foreach (int i in splitedLineIntegers)
                    units[units.Count - 1].Add(i);
            }

            for (int i = 0; i < units.Count; i++)
            {
                for (int j = 0; j < units[i].Count; j++)
                {
                    int unitTmp = units[i][j];

                    if (unitTmp >= 64) // MER
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('M');
                        unitTmp -= 64;
                    }
                    else if (unitTmp >= 32) // FOREST
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('F');
                        unitTmp -= 32;
                    }
                    else
                    {
                        Console.Write('T');

                        bool addUnitInZone (int i, int j, int i2, int j2)
                        {
                            if (i2 >= 0 && i2 < units.Count && j2 >= 0 && j2 < units[i].Count)
                                foreach (List<List<int>> zone in zones)
                                    if (zone.Any(l => l[0] == i2 && l[1] == j2) && !zone.Any(l => l[0] == i && l[1] == j))
                                    {
                                        zone.Add(new List<int> { i, j });
                                        return true;
                                    }
                            return false;
                        }

                        bool addedInAZone = false;

                        if (unitTmp >= Math.Pow(2, 3)) // EAST
                            unitTmp -= (int)Math.Pow(2, 3);
                        else if (addUnitInZone(i, j, i, j + 1))
                            addedInAZone = true;

                        if (unitTmp >= Math.Pow(2, 2) && !addedInAZone) // SUD
                            unitTmp -= (int)Math.Pow(2, 2);
                        else if (addUnitInZone(i, j, i + 1, j))
                            addedInAZone = true;

                        if (unitTmp >= Math.Pow(2, 1) && !addedInAZone) // WEST
                            unitTmp -= (int)Math.Pow(2, 1);
                        else if (addUnitInZone(i, j, i, j - 1))
                            addedInAZone = true;

                        if (unitTmp >= Math.Pow(2, 0) && !addedInAZone) // NORD
                            unitTmp -= (int)Math.Pow(2, 0);
                        else if (addUnitInZone(i, j, i - 1, j))
                            addedInAZone = true;

                        if (!addedInAZone)
                            zones.Add(new List<List<int>> { new List<int> { i, j } });
                    }
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }

            foreach (List<List<int>> zone in zones)
            {
                foreach (List<int> unitXY in zone)
                    Console.Write("[{0}, {1}], ", unitXY[0], unitXY[1]);

                Console.WriteLine();
            }
        }
    }
}