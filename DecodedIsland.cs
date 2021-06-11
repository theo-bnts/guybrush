using System.Collections.Generic;
using System.Linq;

namespace Production
{
    /// <summary>
    /// Classe héritée de la classe Island
    /// Permet de créer une île à partir d'un fichier de type .clair
    /// </summary>
    class DecodedIsland : Island
    {
        /// <summary>
        /// Constructeur de la classe fille DecodedIsland
        /// </summary>
        /// <param name="path">Chemin du fichier</param>
        public DecodedIsland(string path) : base(path)
        {
            List<string> lines = Tools.GetFileLines(path);

            for (var y = 0; y < lines.Count; y++)
                for (var x = 0; x < lines[y].Length; x++)
                {
                    Unit unit = BuildUnit(lines, x, y);

                    if (parcels.Any(p => p.Identifier == lines[y][x]))
                    {
                        int index = parcels.FindIndex(p => p.Identifier == lines[y][x]);
                        parcels[index].Units.Add(unit);
                    }
                    else
                    {
                        parcels.Add(new Parcel(unit.Type, lines[y][x]));
                        parcels[parcels.Count - 1].Units.Add(unit);
                    }
                }
        }

        /// <summary>
        /// Permet de construire une unité en déterminant ses frontières et son type
        /// </summary>
        /// <param name="lines">Liste des lignes du fichier de la carte</param>
        /// <param name="x">Abscisse de l'unité</param>
        /// <param name="y">Ordonnée de l'unité</param>
        /// <returns></returns>
        private Unit BuildUnit(List<string> lines, int x, int y)
        {
            char type;

            List<char> borders = new List<char> { };

            if (y - 1 < 0 || lines[y][x] != lines[y - 1][x])
                borders.Add('N');

            if (x - 1 < 0 || lines[y][x] != lines[y][x - 1])
                borders.Add('W');

            if (y + 1 >= lines.Count || lines[y][x] != lines[y + 1][x])
                borders.Add('S');

            if (x + 1 >= lines[y].Length || lines[y][x] != lines[y][x + 1])
                borders.Add('E');

            if (lines[y][x] == 'M' || lines[y][x] == 'F')
                type = lines[y][x];
            else
                type = 'G';

            return new Unit(x, y, type, borders);
        }
    }
}
