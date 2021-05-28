using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Production
{
    class EncryptedIsland
    {
        string path;
        StreamReader buffer;
        string fileLine;
        string decrypted;
        string encrypted;
        string[] lines;
        List<List<int>> cells;
        List<List<List<int>>> zones;


        public EncryptedIsland(string p)
        {
            path = p;
        }

        private void GetFileBuffer()
        {
            try
            {
                buffer = new StreamReader(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        public void GetFileLine()
        {
            this.GetFileBuffer();

            fileLine = buffer.ReadLine();
            encrypted = fileLine;
        }

        public void Decrypt()
        {
            cells = new List<List<int>>();
            zones = new List<List<List<int>>>();

            lines = fileLine.Split('|');

            foreach (string line in lines)
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

        public void Display()
        {
            Console.WriteLine("Encrypted map:\n{0}\n\nDecrypted map:", encrypted);

            const string alphabet = "abcdefghijklmnopqrstuvwxyz";

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
                        Console.Write(cellsDisplay[j, i]);
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

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
            }
        }
    }
}
