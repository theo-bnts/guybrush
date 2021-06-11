using System;
using System.Collections.Generic;

namespace Production
{
    /// <summary>
    /// Classe héritée de la classe Island
    /// Permet de créer une île à partir d'un fichier de type .chiffre
    /// </summary>
    class EncodedIsland : Island
    {
        /// <summary>
        /// Constructeur de la classe fille EncodedIsland
        /// </summary>
        /// <param name="path">Chemin du fichier</param>
        public EncodedIsland(string path) : base(path)
        {
            BuildUnits(path);
        }

        /// <summary>
        /// Création de toutes les unités de l'île
        /// </summary>
        /// <param name="path">Chemin du fichier</param>
        private void BuildUnits(string path)
        {
            List<string> lines = Tools.GetFileLines(path);
            List<Unit> units = new List<Unit> { };

            int y = 0;
            int x = 0;

            foreach (string line in lines[0].Split('|'))
            {
                string[] values = line.Split(':');

                foreach (string value in values)
                {
                    if (value.Length > 0)
                    {
                        Unit unit = new Unit(x, y, Convert.ToInt16(value));
                        units.Add(unit);

                        x++;
                    }
                }

                y++;
                x = 0;
            }

            BuildParcels(units);
        }

        /// <summary>
        /// Création des parcelles de l'île
        /// </summary>
        /// <param name="units">Unités précédemment construites</param>
        private void BuildParcels(List<Unit> units)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";


            char parcelIdentifier;
            int parcelIdentifierIndex = 0;

            while (units.Count > 0)
            {
                List<Unit> newNeighborsUnits = new List<Unit> { units[0] };

                if (newNeighborsUnits[0].Type == 'G')
                {
                    parcelIdentifier = alphabet[parcelIdentifierIndex];
                    parcelIdentifierIndex++;
                }
                else
                    parcelIdentifier = newNeighborsUnits[0].Type;

                Parcel parcel = new Parcel(newNeighborsUnits[0].Type, parcelIdentifier);
                parcels.Add(parcel);

                while (newNeighborsUnits.Count > 0)
                {
                    parcel.Units.Add(newNeighborsUnits[0]);

                    FindNeighbors(units, ref newNeighborsUnits);

                    units.Remove(newNeighborsUnits[0]);
                    newNeighborsUnits.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Trouve les unités voisines qui sont dans la même parcelle
        /// </summary>
        /// <param name="units">Liste des unités précédemment construites</param>
        /// <param name="newNeighborsUnits">Liste des nouvelles unités voisines</param>
        private void FindNeighbors(List<Unit> units, ref List<Unit> newNeighborsUnits)
        {
            char[] defaultBorders = new char[] { 'N', 'W', 'S', 'E' };

            int neighbourX;
            int neighbourY;

            foreach (char border in defaultBorders)
            {
                if (!newNeighborsUnits[0].IsBorderIn(border))
                {
                    switch (border)
                    {
                        case 'N':
                            neighbourX = newNeighborsUnits[0].X;
                            neighbourY = newNeighborsUnits[0].Y - 1;
                            break;
                        case 'W':
                            neighbourX = newNeighborsUnits[0].X - 1;
                            neighbourY = newNeighborsUnits[0].Y;
                            break;
                        case 'S':
                            neighbourX = newNeighborsUnits[0].X;
                            neighbourY = newNeighborsUnits[0].Y + 1;
                            break;
                        case 'E':
                            neighbourX = newNeighborsUnits[0].X + 1;
                            neighbourY = newNeighborsUnits[0].Y;
                            break;
                        default:
                            neighbourX = -1;
                            neighbourY = -1;
                            break;
                    }

                    if (units.Exists(u => u.X == neighbourX && u.Y == neighbourY)
                    && !newNeighborsUnits.Exists(u => u.X == neighbourX && u.Y == neighbourY))
                    {
                        Unit unit = units.Find(u => u.X == neighbourX && u.Y == neighbourY);
                        newNeighborsUnits.Add(unit);
                    }
                }
            }
        }
    }
}
