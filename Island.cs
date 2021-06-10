using System;
using System.IO;
using System.Collections.Generic;

namespace Production
{
    abstract class Island
    {
        #region Attributs
        /// <summary>
        /// Chemin de l'île sans l'exstenion de fichier
        /// </summary>
        protected string pathWithoutExtension;
        
        /// <summary>
        /// Liste des parcelles
        /// </summary>
        protected List<Parcel> parcels;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe mère Island
        /// </summary>
        protected Island()
        {
            parcels = new List<Parcel> {};
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Affiche la liste des parcelles de terre
        /// </summary>
        public void DisplayParcels()
        {
            foreach (Parcel parcel in parcels)
                if (parcel.Type == 'G')
                    parcel.Display();
        }

        /// <summary>
        /// Affiche la liste des parcelles de terre d'une taille minimale
        /// </summary>
        /// <param name="minSize">Taille minimale</param>
        public void DisplayParcels(int minSize)
        {
            bool parcelFinded = false;

            Console.WriteLine("Parcels of size greater or equal to {0} :", minSize);

            foreach (Parcel parcel in parcels)
                if (parcel.Units.Count >= minSize && parcel.Type == 'G')
                {
                    Console.WriteLine("Parcel {0}: {1} units", parcel.Identifier, parcel.Units.Count);
                    parcelFinded = true;
                }

            if (!parcelFinded)
                Console.WriteLine("No parcel");

            Console.WriteLine();
        }

        /// <summary>
        /// Afficher la taille d'une parcelle donnée
        /// </summary>
        /// <param name="parcelIdentifier">Nom de la parcelle</param>
        public void DisplayParcelSize(char parcelIdentifier)
        {
            bool exist = false;
            int parcelSize = 0;

            foreach (Parcel parcel in parcels)
                if (parcel.Identifier == parcelIdentifier)
                {
                    parcelSize = parcel.Units.Count;
                    exist = true;
                }

            if (exist == false)
                Console.WriteLine("Parcel {0} : non-existent", parcelIdentifier);

            Console.WriteLine("Parcel {0} size : {1} units\n", parcelIdentifier, parcelSize);
        }

        /// <summary>
        /// Afficher et retourner la taille moyenne des parcelles
        /// </summary>
        /// <returns>Taille moyenne des parcelles</returns>
        public double DisplayAverageParcelsSize()
        {
            int sum = 0;

            List<Parcel> groundParcels = parcels.FindAll(p => p.Type == 'G');

            foreach (Parcel parcel in groundParcels)
                sum += parcel.Units.Count;

            double value = Math.Round((double)sum / groundParcels.Count, 2); 

            Console.WriteLine("Average area : {0}", value);
            Console.WriteLine();

            return value;
        }

        /// <summary>
        /// Afficher et enregistrer la carte décodée
        /// </summary>
        public void DisplayAndSaveDecodedMap()
        {
            char[,] bitmap = new char[10, 10];
            string path;
            List<string> lines = new List<string> { };
            string line;

            foreach (Parcel parcel in parcels)
                foreach (Unit unit in parcel.Units)
                    bitmap[unit.Y, unit.X] = parcel.Identifier;

            for (int i = 0; i < bitmap.GetLength(0); i++)
            {
                line = "";

                for (int j = 0; j < bitmap.GetLength(1); j++)
                {
                    if (bitmap[i, j] == 'M')
                        Console.ForegroundColor = ConsoleColor.Blue;

                    if (bitmap[i, j] == 'F')
                        Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write(bitmap[i, j]);

                    Console.ResetColor();

                    line += bitmap[i, j];
                }

                Console.WriteLine();

                lines.Add(line);
            }

            Console.WriteLine();

            path = pathWithoutExtension + ".clair";

            Tools.WriteFileLines(path, lines);
        }

        /// <summary>
        /// Afficher et enregistrer la carte encodée
        /// </summary>
        public void DisplayAndSaveEncodedMap()
        {
            int[,] bitmap = new int[10, 10];
            string path;
            List<string> lines = new List<string> { };
            string line = "";

            foreach (Parcel parcel in parcels)
                foreach (Unit unit in parcel.Units)
                    bitmap[unit.Y, unit.X] = unit.Value;

            for (int i = 0; i < bitmap.GetLength(0); i++)
            {
                for (int j = 0; j < bitmap.GetLength(1); j++)
                {
                    line += bitmap[i, j];

                    if (j < bitmap.GetLength(1) - 1)
                        line += ':';
                }

                line += '|';
            }

            Console.WriteLine(line);
            Console.WriteLine();

            lines.Add(line);

            path = pathWithoutExtension + ".chiffre";

            Tools.WriteFileLines(path, lines);
        }
        #endregion
    }
}
