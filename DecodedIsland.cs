using System.Collections.Generic;
using System.Linq;

namespace Production
{
    class DecodedIsland : Island
    {
        public DecodedIsland(string path) : base()
        {
            BuildParcelsFromFile(path);
        }

        private void BuildParcelsFromFile(string path)
        {
            List<string> lines = base.GetFileLines(path);

            for (var y = 0; y < lines.Count; y++)
                for (var x = 0; x < lines[y].Length; x++)
                {
                    List<char> borders = new List<char> { };

                    if (y - 1 < 0 || lines[y][x] != lines[y - 1][x])
                        borders.Add('N');

                    if (x - 1 < 0 || lines[y][x] != lines[y][x - 1])
                        borders.Add('W');

                    if (y + 1 >= lines.Count || lines[y][x] != lines[y + 1][x])
                        borders.Add('S');

                    if (x + 1 >= lines[y].Length || lines[y][x] != lines[y][x + 1])
                        borders.Add('E');

                    char type;

                    if (lines[y][x] == 'M' || lines[y][x] == 'F')
                        type = lines[y][x];
                    else
                        type = 'G';

                    Unit unit = new Unit(x, y, type, borders);

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
    }
}
