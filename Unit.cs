using System;
using System.Collections.Generic;

namespace Production
{
    class Unit
    {
        char type;
        public char Type { get => type; }

        int x;
        public int X { get => x; }

        int y;
        public int Y { get => y; }

        int value;
        public int Value { get => value; }

        List<char> borders;

        public Unit(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.value = value;

            FindType();
            FindBorders();
        }

        public Unit(int x, int y, char type, List<char> borders)
        {
            this.x = x;
            this.y = y;
            this.type = type;
            this.borders = borders;

            CalculValue();
        }

        private void FindType()
        {
            if (value >= 64)
                type = 'M';
            else if (value >= 32)
                type = 'F';
            else
                type = 'G';
        }

        private void FindBorders()
        {
            int rest = value;

            if (rest >= 64)
                rest -= 64;
            else if (rest >= 32)
                rest -= 32;

            char[] defaultBorderList = new char[] { 'N', 'W', 'S', 'E' };

            borders = new List<char> {};

            for (var i = defaultBorderList.Length - 1; i >= 0; i--)
            {
                int identifier = (int) Math.Pow(2, i);
                char border = defaultBorderList[i];

                if (rest >= identifier)
                {
                    borders.Add(border);
                    rest -= identifier;
                }
            }
        }

        private void CalculValue()
        {
            value = 0;

            foreach (char border in borders)
                switch (border)
                {
                    case 'N': value += 1; break;
                    case 'W': value += 2; break;
                    case 'S': value += 4; break;
                    case 'E': value += 8; break;
                }

            switch (type)
            {
                case 'F': value += 32; break;
                case 'M': value += 64; break;
            }
        }

        public bool IsBorderIn(char location)
        {
            if (borders.Contains(location))
                return true;
            else
                return false;
        }

        public string DisplayCoordinates()
        {
            return $"({x},{y})";
        }
    }
}
