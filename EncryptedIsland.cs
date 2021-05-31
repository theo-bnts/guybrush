using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Production
{
    class EncryptedIsland
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        string fileLine;
        List<List<int>> cells;
        List<List<List<int>>> zones;

        public EncryptedIsland(string path)
        {
            this.GetFileLine(path);
        }

        private void GetFileLine(string path)
        {
            StreamReader file;

            try
            {
                file = new StreamReader(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            fileLine = file.ReadLine();

            file.Close();
        }

        public void Decrypt()
        {
            cells = new List<List<int>>();
            zones = new List<List<List<int>>>();

            foreach (string line in fileLine.Split('|'))
            {
                cells.Add(new List<int> { });

                List<int> splittedLine = line.Split(':').Select(Int32.Parse).ToList();

                foreach (int i in splittedLine)
                    cells[cells.Count - 1].Add(i);
            }

            for (int i = 0; i < cells.Count; i++)
                for (int j = 0; j < cells[i].Count; j++)
                {
                    int cellValue = cells[i][j];

                    if (cellValue >= 64)        // MER
                        cellValue -= 64;
                    else if (cellValue >= 32)   // FOREST
                        cellValue -= 32;
                    else                        // GROUND
                    {
                        bool addedInAZone = false;

                        void AddCellInZone(int i, int j, int i2, int j2)
                        {
                            if (i2 >= 0 && i2 < cells.Count && j2 >= 0 && j2 < cells[i].Count && !addedInAZone)
                                foreach (List<List<int>> zone in zones)
                                    if (zone.Any(l => l[0] == i2 && l[1] == j2) && !zone.Any(l => l[0] == i && l[1] == j))
                                    {
                                        zone.Add(new List<int> { i, j });
                                        addedInAZone = true;
                                    }
                        }

                        if (cellValue >= Math.Pow(2, 3))                    // EAST
                            cellValue -= (int)Math.Pow(2, 3);
                        else
                            AddCellInZone(i, j, i, j + 1);

                        if (cellValue >= Math.Pow(2, 2) && !addedInAZone)   // SUD
                            cellValue -= (int)Math.Pow(2, 2);
                        else
                            AddCellInZone(i, j, i + 1, j);

                        if (cellValue >= Math.Pow(2, 1) && !addedInAZone)   // WEST
                            cellValue -= (int)Math.Pow(2, 1);
                        else
                            AddCellInZone(i, j, i, j - 1);

                        if (cellValue >= Math.Pow(2, 0) && !addedInAZone)   // NORD
                            cellValue -= (int)Math.Pow(2, 0);
                        else
                            AddCellInZone(i, j, i - 1, j);

                        if (!addedInAZone)
                            zones.Add(new List<List<int>> { new List<int> { i, j } });
                    }
                }
        }

        public void DisplayDecryptedMap()
        {
            Console.WriteLine("Decrypted map:");

            char[,] cellsDisplay = new char[cells.Count, cells[0].Count];

            for (int i = 0; i < zones.Count; i++)
                for (int j = 0; j < zones[i].Count; j++)
                {
                    int x = zones[i][j][0];
                    int y = zones[i][j][1];

                    cellsDisplay[y, x] = alphabet[i];
                }

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    if (alphabet.Contains(cellsDisplay[j, i]))
                    {
                        Console.Write(cellsDisplay[j, i]);
                    }
                    else if (cells[i][j] >= 64)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('M');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('F');
                    }

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private int GetZoneSize(int zoneIndex)
        {
            List<List<int>> zone = zones[zoneIndex];

            return zone.Count();
        }

        private void DisplayZone(int zoneIndex)
        {
            Console.Write("{0} [{1} cells] > ", alphabet[zoneIndex], GetZoneSize(zoneIndex));

            foreach (List<int> cellXY in zones[zoneIndex])
                Console.Write("({0},{1}); ", cellXY[0], cellXY[1]);

            Console.WriteLine();
        }

        public void DisplayZones()
        {
            Console.WriteLine("Zones:");

            for (var i = 0; i < zones.Count(); i++)
                this.DisplayZone(i);

            Console.WriteLine();
        }

        public void DisplayZonesIndentifiers(int minimumSize)
        {
            Console.Write("Identiants des zones contenant au moins {0} cellule{1}: ", minimumSize, minimumSize > 1 ? 's' : null);

            for (var i = 0; i < zones.Count(); i++)
                if (GetZoneSize(i) >= minimumSize)
                    Console.Write("{0}; ", alphabet[i]);

            Console.WriteLine();
            Console.WriteLine();
        }

        public double GetAverageZonesSize()
        {
            int zonesCount = zones.Count();
            double sum = 0;

            for (var i = 0; i < zonesCount; i++)
                sum += GetZoneSize(i);

            return Math.Round(sum / zonesCount, 2);
        }
    }
}