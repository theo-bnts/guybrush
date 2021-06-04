using System;
using System.Collections.Generic;

namespace Production
{
    class Unit
    {
        #region Attributs
        /// <summary>
        /// Type
        /// </summary>
        char type;

        /// <summary>
        /// Abscise
        /// </summary>
        int x;
        
        /// <summary>
        /// Ordonnée
        /// </summary>
        int y;
        
        /// <summary>
        /// Valeur
        /// </summary>
        int value;
        
        /// <summary>
        /// Liste des frontières
        /// </summary>
        List<char> borders;
        #endregion

        #region Accesseurs
        /// <summary>
        /// Accesseur en lecture pour l'attribut type
        /// </summary>
        public char Type { get => type; }

        /// <summary>
        /// Accesseur en lecture pour l'attribut x
        /// </summary>
        public int X { get => x; }

        /// <summary>
        /// Accesseur en lecture pour l'attribut y
        /// </summary>
        public int Y { get => y; }

        /// <summary>
        /// Accesseur en lecture pour l'attribut value
        /// </summary>
        public int Value { get => value; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe Unit
        /// </summary>
        /// <param name="x">Abscise</param>
        /// <param name="y">Ordonnée</param>
        /// <param name="value">Valeur</param>
        public Unit(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.value = value;

            FindType();
            FindBorders();
        }

        /// <summary>
        /// Décomposition du constructeur de la classe Unit
        /// Détermine le type de l'unité
        /// </summary>
        private void FindType()
        {
            if (value >= 64)
                type = 'M';
            else if (value >= 32)
                type = 'F';
            else
                type = 'G';
        }

        /// <summary>
        /// Suite de la décomposition du constructeur de la classe Unit
        /// Détermine les frontières de l'unité
        /// </summary>
        private void FindBorders()
        {
            int rest = value;

            if (rest >= 64)
                rest -= 64;
            else if (rest >= 32)
                rest -= 32;

            char[] defaultBorderList = new char[] { 'N', 'W', 'S', 'E' };

            borders = new List<char> { };

            for (var i = defaultBorderList.Length - 1; i >= 0; i--)
            {
                int identifier = (int)Math.Pow(2, i);
                char border = defaultBorderList[i];

                if (rest >= identifier)
                {
                    borders.Add(border);
                    rest -= identifier;
                }
            }
        }

        /// <summary>
        /// Surcharge du constructeur de la classe Unit
        /// </summary>
        /// <param name="x">Abscise</param>
        /// <param name="y">Ordonnée</param>
        /// <param name="type">Type</param>
        /// <param name="borders">Liste des frontières</param>
        public Unit(int x, int y, char type, List<char> borders)
        {
            this.x = x;
            this.y = y;
            this.type = type;
            this.borders = borders;

            CalculValue();
        }

        /// <summary>
        /// Décomposition de la surcharge du constructeur de la classe Unit
        /// Détermine la valeur de l'unité
        /// </summary>
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
        #endregion

        #region Méthodes
        /// <summary>
        /// Retourne s'il y a une frontière dans la direction specifiée
        /// </summary>
        /// <param name="location">Direction</param>
        /// <returns>Présence de la frontière</returns>
        public bool IsBorderIn(char location)
        {
            if (borders.Contains(location))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Retoune l'affichage des coordonnées de l'objet
        /// </summary>
        /// <returns>Affichage des coordonnées</returns>
        public string DisplayCoordinates()
        {
            return $"({x},{y})";
        }
        #endregion
    }
}
