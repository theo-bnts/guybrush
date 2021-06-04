using System;
using System.Linq;
using System.Collections.Generic;

namespace Production
{
    class Parcel
    {
        #region Attributs
        /// <summary>
        /// Type
        /// </summary>
        char type;

        /// <summary>
        /// Identifiant
        /// </summary>
        char identifier;
        
        /// <summary>
        /// Liste des unités
        /// </summary>
        List<Unit> units;
        #endregion

        #region Accesseurs
        /// <summary>
        /// Accesseur en lecture pour l'attribut type
        /// </summary>
        public char Type { get => type; }

        /// <summary>
        /// Accesseur en lecture pour l'attribut identifier
        /// </summary>
        public char Identifier { get => identifier; }

        /// <summary>
        /// Accesseur en lecture et écriture pour l'attribut units
        /// </summary>
        public List<Unit> Units { get => units; set => units = value; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe Parcel
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="identifier">Identifiant</param>
        public Parcel(char type, char identifier)
        {
            this.type = type;
            this.identifier = identifier;
            units = new List<Unit> {};
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Afficher l'identifiant, la taille et les unités de l'objet
        /// </summary>
        public void Display()
        {
            Console.WriteLine(
                "PARCEL {0} - {1} units\n{2}\n",
                identifier,
                units.Count,
                String.Join(" ", units.Select(u => u.DisplayCoordinates()))
            );
        }
        #endregion
    }
}
