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
            BuildUnitsFromFile(path);
        }

        /// <summary>
        /// Décomposition du constructeur de la classe EncodedIsland
        /// Construire les objets unités de l'île
        /// </summary>
        /// <param name="path">Chemin du fichier</param>
        private void BuildUnitsFromFile(string path)
        {
            List<string> lines = base.GetFileLines(path);

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

            bool addedInAParcel;

            foreach (Unit unit in units)
            {
                addedInAParcel = false;

                foreach (char border in defaultBorders)
                    foreach (Parcel parcel in parcels)
                        if (!unit.IsBorderIn(border))
                        {
                            switch (border)
                            {
                                case 'N':
                                    neighbourX = unit.X;
                                    neighbourY = unit.Y - 1;
                                    break;
                                case 'W':
                                    neighbourX = unit.X - 1;
                                    neighbourY = unit.Y;
                                    break;
                                case 'S':
                                    neighbourX = unit.X;
                                    neighbourY = unit.Y + 1;
                                    break;
                                case 'E':
                                    neighbourX = unit.X + 1;
                                    neighbourY = unit.Y;
                                    break;
                                default:
                                    neighbourX = -1;
                                    neighbourY = -1;
                                    break;
                            }

                            foreach (Unit secondUnit in parcel.Units.ToArray())
                            {
                                if (!addedInAParcel && neighbourX == secondUnit.X && neighbourY == secondUnit.Y)
                                {
                                    parcel.Units.Add(unit);
                                    addedInAParcel = true;
                                }
                            }
                        }

                if (!addedInAParcel)
                {
                    if (unit.Type == 'G')
                    {
                        parcelIdentifier = alphabet[parcelIdentifierIndex];
                        parcelIdentifierIndex++;
                    }
                    else
                        parcelIdentifier = unit.Type;

                    parcels.Add(new Parcel(unit.Type, parcelIdentifier));
                    parcels[parcels.Count - 1].Units.Add(unit);
                }
                    
            }
        }
    }
}
