using System;
using System.Linq;
using System.Collections.Generic;

namespace Production
{
    class Parcel
    {
        char type;
        public char Type { get => type; }

        char identifier;
        public char Identifier { get => identifier; }

        List<Unit> units;
        public List<Unit> Units { get => units; set => units = value; }

        public Parcel(char type, char identifier)
        {
            this.type = type;
            this.identifier = identifier;
            units = new List<Unit> {};
        }

        public void Display()
        {
            Console.WriteLine(
                "PARCEL {0} - {1} units\n{2}\n",
                identifier,
                units.Count,
                String.Join(" ", units.Select(u => u.DisplayCoordinates()))
            );
        }
    }
}
