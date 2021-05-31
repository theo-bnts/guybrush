using System;

namespace Production
{
    class Program
    {
        static void Main(string[] args)
        {
            DecryptedIsland i = new DecryptedIsland("../../../islands/Scabb.clair");

            i.GetFileLines();

            i.Encrypt();

            i.Display();



            EncryptedIsland j = new EncryptedIsland ("../../../islands/Scabb.chiffre");

            j.Decrypt();

            j.DisplayDecryptedMap();

            j.DisplayZones();

            j.DisplayZonesIndentifiers(5);

            Console.WriteLine("Taille motenne des zones: {0}", j.GetAverageZonesSize());
        }
    }
}