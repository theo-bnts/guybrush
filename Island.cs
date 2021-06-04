using System;
using System.IO;
using System.Collections.Generic;

namespace Production
{
    abstract class Island
    {
        protected List<Parcel> parcels;

        public Island()
        {
            parcels = new List<Parcel> {};
        }

        protected List<string> GetFileLines(string path)
        {
            StreamReader file;
            List<string> lines = new List<string> {};

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

        public void DisplayParcels()
        {
            foreach (Parcel parcel in parcels)
                if (parcel.Type == 'G')
                    parcel.Display();
        }

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

        public void DisplayDecodedMap()
        {
            char[,] bitmap = new char[10, 10];

            foreach (Parcel parcel in parcels)
                foreach (Unit unit in parcel.Units)
                    bitmap[unit.Y, unit.X] = parcel.Identifier;

            for (int i = 0; i < bitmap.GetLength(0); i++)
            {
                for (int j = 0; j < bitmap.GetLength(1); j++)
                {
                    if (bitmap[i, j] == 'M')
                        Console.ForegroundColor = ConsoleColor.Blue;

                    if (bitmap[i, j] == 'F')
                        Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write(bitmap[i, j]);

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void DisplayEncodedMap()
        {
            int[,] bitmap = new int[10, 10];

            foreach (Parcel parcel in parcels)
                foreach (Unit unit in parcel.Units)
                    bitmap[unit.Y, unit.X] = unit.Value;

            for (int i = 0; i < bitmap.GetLength(0); i++)
            {
                for (int j = 0; j < bitmap.GetLength(1); j++)
                {
                    Console.Write(bitmap[i, j]);

                    if (j < bitmap.GetLength(1) - 1)
                        Console.Write(':');
                }

                Console.Write('|');
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
