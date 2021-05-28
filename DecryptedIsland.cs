using System;
using System.IO;
using System.Collections.Generic;

namespace Production
{
    class DecryptedIsland
    {
        string path;
        StreamReader buffer;
        List<string> fileLines;
        string decrypted;
        string encrypted;

        public DecryptedIsland(string p)
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

        public void GetFileLines()
        {
            this.GetFileBuffer();

            string line;

            fileLines = new List<string>();

            while ((line = buffer.ReadLine()) != null)
                fileLines.Add(line);

            decrypted = String.Join("\n", fileLines.ToArray());
        }

        public void Encrypt()
        {
            double cell;

            for (var i = 0; i < fileLines.Count; i++)
            {
                for (var j = 0; j < fileLines[i].Length; j++)
                {
                    cell = 0;

                    if (i - 1 < 0 || fileLines[i][j] != fileLines[i - 1][j]) // NORD
                        cell += Math.Pow(2, 0);

                    if (j - 1 < 0 || fileLines[i][j] != fileLines[i][j - 1]) // WEST
                        cell += Math.Pow(2, 1);

                    if (i + 1 >= fileLines.Count || fileLines[i][j] != fileLines[i + 1][j]) // SUD
                        cell += Math.Pow(2, 2);

                    if (j + 1 >= fileLines[i].Length || fileLines[i][j] != fileLines[i][j + 1]) // EAST
                        cell += Math.Pow(2, 3);

                    if (fileLines[i][j] == 'M') // MER
                        cell += 64;

                    if (fileLines[i][j] == 'F') // FOREST
                        cell += 32;

                    encrypted += cell;

                    if (j < fileLines[j].Length - 1)
                        encrypted += ':';
                }

                if (i < fileLines[i].Length - 1)
                    encrypted += '|';
            }
        }

        public void Display()
        {
            Console.WriteLine("Decrypted map:\n{0}\n\nEncrypted map:\n{1}", decrypted, encrypted);
        }
    }
}
