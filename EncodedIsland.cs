using System;
using System.Collections.Generic;

namespace Production
{
    class EncodedIsland : Island
    {
        /// <summary>
        /// Constructeur de la classe fille EncodedIsland
        /// </summary>
        /// <param name="path">Chemin du fichier</param>
        public EncodedIsland(string path) : base()
        {
            pathWithoutExtension = path.Substring(0, path.LastIndexOf('.'));

            BuildUnitsFromFile(path);
        }

        /// <summary>
        /// Décomposition du constructeur de la classe EncodedIsland
        /// Construire les objets unités de l'île
        /// </summary>
        /// <param name="path">Chemin du fichier</param>
        private void BuildUnitsFromFile(string path)
        {
            List<string> lines = Tools.GetFileLines(path);

            int y = 0;
            int x;

            List<Unit> units = new List<Unit> {};

            foreach (string line in lines[0].Split('|'))
            {
                x = 0;

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
            }

            BuildParcelsAndAddUnits(units);
        }

        /// <summary>
        /// Suite de la décomposition du constructeur de la classe EncodedIsland
        /// Construire les parcelles et classer les unités dans celle-ci
        /// </summary>
        /// <param name="units">Unités précédemment construites</param>
        private void BuildParcelsAndAddUnits(List<Unit> units)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            char[] defaultBorders = new char[] { 'N', 'W', 'S', 'E' };

            char parcelIdentifier;
            int parcelIdentifierIndex = 0;

            int neighbourX;
            int neighbourY;

            while (units.Count > 0)
            {
                List<Unit> unitsInTheSameParcel = new List<Unit> { units[0] };

                if (unitsInTheSameParcel[0].Type == 'G')
                {
                    parcelIdentifier = alphabet[parcelIdentifierIndex];
                    parcelIdentifierIndex++;
                }
                else
                    parcelIdentifier = unitsInTheSameParcel[0].Type;

                Parcel parcel = new Parcel(unitsInTheSameParcel[0].Type, parcelIdentifier);
                parcels.Add(parcel);

                while (unitsInTheSameParcel.Count > 0)
                {
                    parcel.Units.Add(unitsInTheSameParcel[0]);

                    foreach (char border in defaultBorders)
                    {
                        if (!unitsInTheSameParcel[0].IsBorderIn(border))
                        {
                            switch (border)
                            {
                                case 'N':
                                    neighbourX = unitsInTheSameParcel[0].X;
                                    neighbourY = unitsInTheSameParcel[0].Y - 1;
                                    break;
                                case 'W':
                                    neighbourX = unitsInTheSameParcel[0].X - 1;
                                    neighbourY = unitsInTheSameParcel[0].Y;
                                    break;
                                case 'S':
                                    neighbourX = unitsInTheSameParcel[0].X;
                                    neighbourY = unitsInTheSameParcel[0].Y + 1;
                                    break;
                                case 'E':
                                    neighbourX = unitsInTheSameParcel[0].X + 1;
                                    neighbourY = unitsInTheSameParcel[0].Y;
                                    break;
                                default:
                                    neighbourX = -1;
                                    neighbourY = -1;
                                    break;
                            }

                            if (units.Exists(u => u.X == neighbourX && u.Y == neighbourY))
                            {
                                unitsInTheSameParcel.Add(
                                    units.Find(u => u.X == neighbourX && u.Y == neighbourY)
                                );
                            }
                        }
                    }

                    units.Remove(unitsInTheSameParcel[0]);
                    unitsInTheSameParcel.RemoveAt(0);
                }
            }
        }
    }
}
