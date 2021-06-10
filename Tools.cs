using System;
using System.IO;
using System.Collections.Generic;

namespace Production
{
    static class Tools
    {
        /// <summary>
        /// Lire et retourner les lignes du fichier
        /// </summary>
        /// <param name="path">Chemin d'accès du fichier</param>
        /// <returns>Liste de lignes</returns>
        public static List<string> GetFileLines(string path)
        {
            StreamReader file;
            List<string> lines = new List<string> { };

            try
            {
                file = new StreamReader(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            string line;
            while ((line = file.ReadLine()) != null)
                lines.Add(line);

            file.Close();

            return lines;
        }

        /// <summary>
        /// Ecrire les lignes du fichier
        /// </summary>
        /// <param name="path">Chemin d'accès du fichier</param>
        /// <param name="lines">Liste de lignes</param>
        public static void WriteFileLines(string path, List<string> lines)
        {
            StreamWriter file;

            try
            {
                file = new StreamWriter(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            foreach (string line in lines)
                file.WriteLine(line);

            file.Close();
        }
    }
}
